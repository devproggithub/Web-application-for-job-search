using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploiApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNomPrenom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Id_Candidat",
                table: "Utilisateurs");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Recruteurs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Recruteurs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Candidats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Candidats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Recruteurs");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Recruteurs");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Candidats");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Candidats");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Candidat",
                table: "Utilisateurs",
                type: "int",
                nullable: true);
        }
    }
}
