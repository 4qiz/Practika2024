using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class mockdata4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e4bc3de-c4ce-4a7f-bb4b-95b560d0aef9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dcb1ff8-7a20-4775-80cb-7ab0b694eee4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d50e04d-6e3e-48d7-9593-c0258f909ed1", null, "Admin", "ADMIN" },
                    { "d87f69af-257e-48c9-80b4-ceabc88cc133", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Medicine",
                columns: new[] { "MedicineId", "Image", "Manufacturer", "Name", "Price", "TradeName" },
                values: new object[] { 2, "https://imgs.asna.ru/iblock/177/177882ef988b42be05abd45dbb7d5fba/816f88b93afa5c096afbeec679ffd4c0.jpg", "Татхимфармпрепараты АО", "Кальцекс 500мг 10 шт. таблетки татхимфарм", 42m, "Кальцекс" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d50e04d-6e3e-48d7-9593-c0258f909ed1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d87f69af-257e-48c9-80b4-ceabc88cc133");

            migrationBuilder.DeleteData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e4bc3de-c4ce-4a7f-bb4b-95b560d0aef9", null, "User", "USER" },
                    { "9dcb1ff8-7a20-4775-80cb-7ab0b694eee4", null, "Admin", "ADMIN" }
                });
        }
    }
}
