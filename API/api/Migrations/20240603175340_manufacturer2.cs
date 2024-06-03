using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class manufacturer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a10f8a4-3c44-4f3b-8c45-0ece22771d47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adaacfaf-f7a4-4f58-932c-6bc9fbcce7e2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "612087f8-61c8-4925-bf21-330511f0b951", null, "User", "USER" },
                    { "a799c9e2-583b-44c6-90de-6a8cf2e8480a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "ManufacturerId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "612087f8-61c8-4925-bf21-330511f0b951");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a799c9e2-583b-44c6-90de-6a8cf2e8480a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a10f8a4-3c44-4f3b-8c45-0ece22771d47", null, "Admin", "ADMIN" },
                    { "adaacfaf-f7a4-4f58-932c-6bc9fbcce7e2", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "ManufacturerId");
        }
    }
}
