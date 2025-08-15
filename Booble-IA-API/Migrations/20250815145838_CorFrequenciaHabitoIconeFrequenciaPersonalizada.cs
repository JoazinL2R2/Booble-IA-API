using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Booble_IA_API.Migrations
{
    /// <inheritdoc />
    public partial class CorFrequenciaHabitoIconeFrequenciaPersonalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Des_Icone",
                table: "TAB_Habito",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Des_Cor",
                table: "TAB_Habito",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "Idf_Usuario",
                table: "TAB_Habito",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RankingStreaks",
                columns: table => new
                {
                    Idf_Usuario = table.Column<int>(type: "integer", nullable: false),
                    Des_Nme = table.Column<string>(type: "text", nullable: false),
                    MaxStreak = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TAB_Cor",
                columns: table => new
                {
                    Idf_Cor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Des_Cor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Cor", x => x.Idf_Cor);
                    table.UniqueConstraint("AK_TAB_Cor_Des_Cor", x => x.Des_Cor);
                });

            migrationBuilder.CreateTable(
                name: "TAB_Frequencia",
                columns: table => new
                {
                    Idf_Frequencia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Des_Frequencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Frequencia", x => x.Idf_Frequencia);
                });

            migrationBuilder.CreateTable(
                name: "TAB_Frequencia_Personalizada",
                columns: table => new
                {
                    Idf_Frequencia_Personalizada = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Idf_Frequencia = table.Column<int>(type: "integer", nullable: false),
                    Des_Frequencia_Personalizada = table.Column<int>(type: "integer", nullable: false),
                    Dta_Frequencias = table.Column<string>(type: "jsonb", nullable: false),
                    Dta_Cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Frequencia_Personalizada", x => x.Idf_Frequencia_Personalizada);
                });

            migrationBuilder.CreateTable(
                name: "TAB_Habito_Icone",
                columns: table => new
                {
                    Idf_Habito_Icone = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Des_Habito_Icone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAB_Habito_Icone", x => x.Idf_Habito_Icone);
                    table.UniqueConstraint("AK_TAB_Habito_Icone_Des_Habito_Icone", x => x.Des_Habito_Icone);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Habito_Des_Cor",
                table: "TAB_Habito",
                column: "Des_Cor");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Habito_Des_Icone",
                table: "TAB_Habito",
                column: "Des_Icone");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Habito_Idf_Frequencia",
                table: "TAB_Habito",
                column: "Idf_Frequencia");

            migrationBuilder.CreateIndex(
                name: "IX_TAB_Habito_Idf_Usuario",
                table: "TAB_Habito",
                column: "Idf_Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_Habito_TAB_Cor_Des_Cor",
                table: "TAB_Habito",
                column: "Des_Cor",
                principalTable: "TAB_Cor",
                principalColumn: "Des_Cor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_Habito_TAB_Frequencia_Idf_Frequencia",
                table: "TAB_Habito",
                column: "Idf_Frequencia",
                principalTable: "TAB_Frequencia",
                principalColumn: "Idf_Frequencia",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_Habito_TAB_Frequencia_Personalizada_Idf_Frequencia",
                table: "TAB_Habito",
                column: "Idf_Frequencia",
                principalTable: "TAB_Frequencia_Personalizada",
                principalColumn: "Idf_Frequencia_Personalizada",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_Habito_TAB_Habito_Icone_Des_Icone",
                table: "TAB_Habito",
                column: "Des_Icone",
                principalTable: "TAB_Habito_Icone",
                principalColumn: "Des_Habito_Icone",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TAB_Habito_TAB_Usuario_Idf_Usuario",
                table: "TAB_Habito",
                column: "Idf_Usuario",
                principalTable: "TAB_Usuario",
                principalColumn: "Idf_Usuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TAB_Habito_TAB_Cor_Des_Cor",
                table: "TAB_Habito");

            migrationBuilder.DropForeignKey(
                name: "FK_TAB_Habito_TAB_Frequencia_Idf_Frequencia",
                table: "TAB_Habito");

            migrationBuilder.DropForeignKey(
                name: "FK_TAB_Habito_TAB_Frequencia_Personalizada_Idf_Frequencia",
                table: "TAB_Habito");

            migrationBuilder.DropForeignKey(
                name: "FK_TAB_Habito_TAB_Habito_Icone_Des_Icone",
                table: "TAB_Habito");

            migrationBuilder.DropForeignKey(
                name: "FK_TAB_Habito_TAB_Usuario_Idf_Usuario",
                table: "TAB_Habito");

            migrationBuilder.DropTable(
                name: "RankingStreaks");

            migrationBuilder.DropTable(
                name: "TAB_Cor");

            migrationBuilder.DropTable(
                name: "TAB_Frequencia");

            migrationBuilder.DropTable(
                name: "TAB_Frequencia_Personalizada");

            migrationBuilder.DropTable(
                name: "TAB_Habito_Icone");

            migrationBuilder.DropIndex(
                name: "IX_TAB_Habito_Des_Cor",
                table: "TAB_Habito");

            migrationBuilder.DropIndex(
                name: "IX_TAB_Habito_Des_Icone",
                table: "TAB_Habito");

            migrationBuilder.DropIndex(
                name: "IX_TAB_Habito_Idf_Frequencia",
                table: "TAB_Habito");

            migrationBuilder.DropIndex(
                name: "IX_TAB_Habito_Idf_Usuario",
                table: "TAB_Habito");

            migrationBuilder.DropColumn(
                name: "Idf_Usuario",
                table: "TAB_Habito");

            migrationBuilder.AlterColumn<string>(
                name: "Des_Icone",
                table: "TAB_Habito",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Des_Cor",
                table: "TAB_Habito",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
