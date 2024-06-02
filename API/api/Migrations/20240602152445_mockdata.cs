using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class mockdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fd6cc0e-c622-4d40-937d-89dd53481082");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b2a80f9-2eb2-4704-a77a-012e4ebdde03");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f30b155-8df4-4df6-9280-08bee670368f", null, "User", "USER" },
                    { "cb9dd54f-ac08-46ed-ba67-e454629a30a0", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "WarehouseId", "Name" },
                values: new object[,]
                {
                    { 1, "Склад №2" },
                    { 2, "Склад №2" },
                    { 3, "Склад №3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f30b155-8df4-4df6-9280-08bee670368f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb9dd54f-ac08-46ed-ba67-e454629a30a0");

            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "WarehouseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "WarehouseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "WarehouseId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fd6cc0e-c622-4d40-937d-89dd53481082", null, "User", "USER" },
                    { "5b2a80f9-2eb2-4704-a77a-012e4ebdde03", null, "Admin", "ADMIN" }
                });
        }
    }
}
