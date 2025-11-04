using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EurekaMoviesBE.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                schema: "movieschema",
                table: "Rating",
                newName: "Star");

            migrationBuilder.InsertData(
                schema: "movieschema",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45bfea26-0391-48ce-95a2-41b1f16c2633", null, "Viewer", "VIEWER" },
                    { "5a088ddf-b132-4269-9377-1711b06a2bc1", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "movieschema",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45bfea26-0391-48ce-95a2-41b1f16c2633");

            migrationBuilder.DeleteData(
                schema: "movieschema",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a088ddf-b132-4269-9377-1711b06a2bc1");

            migrationBuilder.RenameColumn(
                name: "Star",
                schema: "movieschema",
                table: "Rating",
                newName: "Start");
        }
    }
}
