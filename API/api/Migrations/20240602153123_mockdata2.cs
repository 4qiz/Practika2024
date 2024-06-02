using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class mockdata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f30b155-8df4-4df6-9280-08bee670368f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb9dd54f-ac08-46ed-ba67-e454629a30a0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "366f7a67-cec3-421e-920c-bf6bbb276da2", null, "Admin", "ADMIN" },
                    { "b8218b15-00e8-44f8-8cf0-e6cf1bfc6741", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Medicine",
                columns: new[] { "MedicineId", "Image", "Manufacturer", "Name", "Price", "TradeName" },
                values: new object[] { 1, "https://imgs.asna.ru/iblock/2d7/2d71cac199086932e4b68e6ae633eca8/100082.jpg", "Новартис Фарма АГ", "Вольтарен 25мг/мл 3мл 5 шт. раствор для внутримышечного введения", 79m, "Вольтарен" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "366f7a67-cec3-421e-920c-bf6bbb276da2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8218b15-00e8-44f8-8cf0-e6cf1bfc6741");

            migrationBuilder.DeleteData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f30b155-8df4-4df6-9280-08bee670368f", null, "User", "USER" },
                    { "cb9dd54f-ac08-46ed-ba67-e454629a30a0", null, "Admin", "ADMIN" }
                });
        }
    }
}
