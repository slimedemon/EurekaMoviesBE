using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurekaMovieBE.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnSecretsToSecret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Secrets",
                schema: "movieschema",
                table: "ClientSecrets",
                newName: "Secret");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Secret",
                schema: "movieschema",
                table: "ClientSecrets",
                newName: "Secrets");
        }
    }
}
