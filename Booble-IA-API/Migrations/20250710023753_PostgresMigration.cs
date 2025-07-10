using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Booble_IA_API.Migrations
{
    /// <inheritdoc />
    public partial class PostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Dta_Nascimento",
                table: "TAB_Usuario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TAB_Amizade",
                columns: table => new
                {
                    Idf_Amizade = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Idf_Usuario_Solicitante = table.Column<int>(type: "integer", nullable: false),
                    Idf_Usuario_Recebedor = table.Column<int>(type: "integer", nullable: false),
                    Dta_Cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Flg_Aceito = table.Column<bool>(type: "boolean", nullable: false),
                    Flg_Inativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Amizade", x => x.Idf_Amizade);
                });

            migrationBuilder.CreateTable(
                name: "TAB_Habito",
                columns: table => new
                {
                    Idf_Habito = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Des_Habito = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Des_Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Flg_Timer = table.Column<bool>(type: "boolean", nullable: true),
                    Timer_Duracao = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    Des_Icone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Num_Xp = table.Column<int>(type: "integer", nullable: false),
                    Flg_Concluido = table.Column<bool>(type: "boolean", nullable: false),
                    Des_Cor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Des_Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Idf_Frequencia = table.Column<int>(type: "integer", nullable: false),
                    Dta_Cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Dta_Conclusoes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Habito", x => x.Idf_Habito);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAB_Amizade");

            migrationBuilder.DropTable(
                name: "TAB_Habito");

            migrationBuilder.DropColumn(
                name: "Dta_Nascimento",
                table: "TAB_Usuario");
        }
    }
}
