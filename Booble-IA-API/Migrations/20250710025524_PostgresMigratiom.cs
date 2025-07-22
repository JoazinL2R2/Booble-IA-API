using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booble_IA_API.Migrations
{
    /// <inheritdoc />
    public partial class PostgresMigratiom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION GetRankingStreak() RETURNS TABLE(Idf_Usuario INT, Des_Nme VARCHAR, MaxStreak INT) AS $$
                BEGIN
                    RETURN QUERY
                    SELECT u.Idf_Usuario, u.Des_Nme,
                        COALESCE(MAX(streak), 0) AS MaxStreak
                    FROM TAB_Usuario u
                    JOIN (
                        SELECT h.Idf_Usuario,
                            MAX(streak) AS streak
                        FROM (
                            SELECT Idf_Usuario, grp, COUNT(*) AS streak
                            FROM (
                                SELECT Idf_Usuario,
                                    concl_dia,
                                    concl_dia - ROW_NUMBER() OVER (PARTITION BY Idf_Usuario ORDER BY concl_dia) * INTERVAL '1 day' AS grp
                                FROM (
                                    SELECT h.Idf_Usuario, CAST(concl AS DATE) AS concl_dia
                                    FROM TAB_Habito h, jsonb_array_elements_text(h.Dta_Conclusoes) concl
                                ) all_concl
                                GROUP BY Idf_Usuario, concl_dia
                            ) streaks
                            GROUP BY Idf_Usuario, grp
                        ) streak_groups
                        GROUP BY Idf_Usuario
                    ) user_streak ON user_streak.Idf_Usuario = u.Idf_Usuario
                    ORDER BY MaxStreak DESC;
                END;
                $$ LANGUAGE plpgsql;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS GetRankingStreak();");
        }
    }
}
