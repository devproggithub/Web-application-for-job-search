using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploiApp.Migrations
{
    /// <inheritdoc />
    public partial class addNiveauDipComLanCV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etablissement",
                table: "Diplomes");

            migrationBuilder.AddColumn<string>(
                name: "Niveau",
                table: "LangueCVs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateObtentation",
                table: "DiplomeCVs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Niveau",
                table: "DiplomeCVs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Niveau",
                table: "CompetenceCVs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "LangueCVs");

            migrationBuilder.DropColumn(
                name: "DateObtentation",
                table: "DiplomeCVs");

            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "DiplomeCVs");

            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "CompetenceCVs");

            migrationBuilder.AddColumn<string>(
                name: "Etablissement",
                table: "Diplomes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
