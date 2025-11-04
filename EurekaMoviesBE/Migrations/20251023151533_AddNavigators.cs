using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurekaMoviesBE.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClientSecrets_ClientId",
                schema: "movieschema",
                table: "ClientSecrets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScopes_ClientId",
                schema: "movieschema",
                table: "ClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantTypes_ClientId",
                schema: "movieschema",
                table: "ClientGrantTypes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResourceScopes_ApiResourceId",
                schema: "movieschema",
                table: "ApiResourceScopes",
                column: "ApiResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceScopes_ApiResources_ApiResourceId",
                schema: "movieschema",
                table: "ApiResourceScopes",
                column: "ApiResourceId",
                principalSchema: "movieschema",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientGrantTypes_Client_ClientId",
                schema: "movieschema",
                table: "ClientGrantTypes",
                column: "ClientId",
                principalSchema: "movieschema",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientScopes_Client_ClientId",
                schema: "movieschema",
                table: "ClientScopes",
                column: "ClientId",
                principalSchema: "movieschema",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSecrets_Client_ClientId",
                schema: "movieschema",
                table: "ClientSecrets",
                column: "ClientId",
                principalSchema: "movieschema",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceScopes_ApiResources_ApiResourceId",
                schema: "movieschema",
                table: "ApiResourceScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientGrantTypes_Client_ClientId",
                schema: "movieschema",
                table: "ClientGrantTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientScopes_Client_ClientId",
                schema: "movieschema",
                table: "ClientScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSecrets_Client_ClientId",
                schema: "movieschema",
                table: "ClientSecrets");

            migrationBuilder.DropIndex(
                name: "IX_ClientSecrets_ClientId",
                schema: "movieschema",
                table: "ClientSecrets");

            migrationBuilder.DropIndex(
                name: "IX_ClientScopes_ClientId",
                schema: "movieschema",
                table: "ClientScopes");

            migrationBuilder.DropIndex(
                name: "IX_ClientGrantTypes_ClientId",
                schema: "movieschema",
                table: "ClientGrantTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApiResourceScopes_ApiResourceId",
                schema: "movieschema",
                table: "ApiResourceScopes");
        }
    }
}
