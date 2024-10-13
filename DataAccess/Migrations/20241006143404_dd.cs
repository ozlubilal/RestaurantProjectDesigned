using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_StoreBills_StoreBillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_StoreBillId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("3cd88cbf-f30a-4b26-a267-0a849c13bc3e"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a86cc775-b11e-4c06-b3b0-7b83ffc3c203"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5a08c31-e362-4954-9110-3523286536c5"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ce101cb0-d460-42dc-9033-fd5350806707"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("65923437-3551-43ed-8b95-265fdffc2ba4"), "Cashier" },
                    { new Guid("8515151a-0e38-4427-94ad-02e3755bed2d"), "Chef" },
                    { new Guid("a16a6bf9-9d5a-4a03-8ed9-935c0e9fdae9"), "Waiter" },
                    { new Guid("ce4aa887-228c-4946-a0f8-66a7ffc2c28f"), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("65923437-3551-43ed-8b95-265fdffc2ba4"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8515151a-0e38-4427-94ad-02e3755bed2d"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a16a6bf9-9d5a-4a03-8ed9-935c0e9fdae9"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ce4aa887-228c-4946-a0f8-66a7ffc2c28f"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3cd88cbf-f30a-4b26-a267-0a849c13bc3e"), "Cashier" },
                    { new Guid("a86cc775-b11e-4c06-b3b0-7b83ffc3c203"), "Waiter" },
                    { new Guid("c5a08c31-e362-4954-9110-3523286536c5"), "Admin" },
                    { new Guid("ce101cb0-d460-42dc-9033-fd5350806707"), "Chef" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StoreBillId",
                table: "Payments",
                column: "StoreBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_StoreBills_StoreBillId",
                table: "Payments",
                column: "StoreBillId",
                principalTable: "StoreBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
