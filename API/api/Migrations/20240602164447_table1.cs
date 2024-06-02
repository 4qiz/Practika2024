using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class table1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseHasMedicine_Medicine_MedicineId",
                table: "WarehouseHasMedicine");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseHasMedicine_Warehouse_WarehouseId",
                table: "WarehouseHasMedicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseHasMedicine",
                table: "WarehouseHasMedicine");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d50e04d-6e3e-48d7-9593-c0258f909ed1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d87f69af-257e-48c9-80b4-ceabc88cc133");

            migrationBuilder.RenameTable(
                name: "WarehouseHasMedicine",
                newName: "WarehouseHasMedicines");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseHasMedicine_WarehouseId",
                table: "WarehouseHasMedicines",
                newName: "IX_WarehouseHasMedicines_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseHasMedicines",
                table: "WarehouseHasMedicines",
                columns: new[] { "MedicineId", "WarehouseId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07d18bb9-99df-4878-a571-35b44b5fbb60", null, "User", "USER" },
                    { "c8b8ccb5-e02e-4c4a-85cf-6c0c43ecbb63", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseHasMedicines_Medicine_MedicineId",
                table: "WarehouseHasMedicines",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "MedicineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseHasMedicines_Warehouse_WarehouseId",
                table: "WarehouseHasMedicines",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseHasMedicines_Medicine_MedicineId",
                table: "WarehouseHasMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseHasMedicines_Warehouse_WarehouseId",
                table: "WarehouseHasMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseHasMedicines",
                table: "WarehouseHasMedicines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07d18bb9-99df-4878-a571-35b44b5fbb60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8b8ccb5-e02e-4c4a-85cf-6c0c43ecbb63");

            migrationBuilder.RenameTable(
                name: "WarehouseHasMedicines",
                newName: "WarehouseHasMedicine");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseHasMedicines_WarehouseId",
                table: "WarehouseHasMedicine",
                newName: "IX_WarehouseHasMedicine_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseHasMedicine",
                table: "WarehouseHasMedicine",
                columns: new[] { "MedicineId", "WarehouseId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d50e04d-6e3e-48d7-9593-c0258f909ed1", null, "Admin", "ADMIN" },
                    { "d87f69af-257e-48c9-80b4-ceabc88cc133", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseHasMedicine_Medicine_MedicineId",
                table: "WarehouseHasMedicine",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "MedicineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseHasMedicine_Warehouse_WarehouseId",
                table: "WarehouseHasMedicine",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
