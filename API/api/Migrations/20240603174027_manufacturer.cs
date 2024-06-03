using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class manufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "822218c2-d8bd-4e04-bdc7-19ee5dec8506");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3450b18-f956-4240-9730-a364ed07bb43");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Medicine");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Medicine",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ManufacturerId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a10f8a4-3c44-4f3b-8c45-0ece22771d47", null, "Admin", "ADMIN" },
                    { "adaacfaf-f7a4-4f58-932c-6bc9fbcce7e2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "ManufacturerId", "Title" },
                values: new object[,]
                {
                    { 1, "Новартис Фарма АГ" },
                    { 2, "Татхимфармпрепараты АО" }
                });

            migrationBuilder.UpdateData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 1,
                column: "ManufacturerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 2,
                column: "ManufacturerId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_ManufacturerId",
                table: "Medicine",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "ManufacturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Manufacturer",
                table: "Medicine");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_ManufacturerId",
                table: "Medicine");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a10f8a4-3c44-4f3b-8c45-0ece22771d47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adaacfaf-f7a4-4f58-932c-6bc9fbcce7e2");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Medicine");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Medicine",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "822218c2-d8bd-4e04-bdc7-19ee5dec8506", null, "User", "USER" },
                    { "f3450b18-f956-4240-9730-a364ed07bb43", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 1,
                column: "Manufacturer",
                value: "Новартис Фарма АГ");

            migrationBuilder.UpdateData(
                table: "Medicine",
                keyColumn: "MedicineId",
                keyValue: 2,
                column: "Manufacturer",
                value: "Татхимфармпрепараты АО");
        }
    }
}
