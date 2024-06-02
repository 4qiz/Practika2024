using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class mockdata3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "366f7a67-cec3-421e-920c-bf6bbb276da2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8218b15-00e8-44f8-8cf0-e6cf1bfc6741");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e4bc3de-c4ce-4a7f-bb4b-95b560d0aef9", null, "User", "USER" },
                    { "9dcb1ff8-7a20-4775-80cb-7ab0b694eee4", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "WarehouseHasMedicine",
                columns: new[] { "MedicineId", "WarehouseId", "Quantity" },
                values: new object[] { 1, 2, 41 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e4bc3de-c4ce-4a7f-bb4b-95b560d0aef9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dcb1ff8-7a20-4775-80cb-7ab0b694eee4");

            migrationBuilder.DeleteData(
                table: "WarehouseHasMedicine",
                keyColumns: new[] { "MedicineId", "WarehouseId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "366f7a67-cec3-421e-920c-bf6bbb276da2", null, "Admin", "ADMIN" },
                    { "b8218b15-00e8-44f8-8cf0-e6cf1bfc6741", null, "User", "USER" }
                });
        }
    }
}
