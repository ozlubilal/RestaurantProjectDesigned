using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class hn : Migration
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
                keyValue: new Guid("15c20fc4-b4f5-476b-8d38-2ab7139b7500"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("52005799-3de1-4b83-9f57-19179e01da35"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("791aeba2-bd10-48f1-829f-6c86002b6ebb"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("cb537f9a-63c4-476c-bdcb-640c66ac335f"));

            migrationBuilder.DropColumn(
                name: "StoreBillId",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("68941640-a296-47b4-aa89-ffee6b03cf39"), "Chef" },
                    { new Guid("d5ed6bdf-0b95-4cb9-89d2-12ed7e3e282c"), "Waiter" },
                    { new Guid("d734f9a4-0a88-407a-bd35-ed0930d20f3d"), "Admin" },
                    { new Guid("f37e9286-616e-4e46-baa3-464767b01716"), "Cashier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("68941640-a296-47b4-aa89-ffee6b03cf39"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d5ed6bdf-0b95-4cb9-89d2-12ed7e3e282c"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d734f9a4-0a88-407a-bd35-ed0930d20f3d"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("f37e9286-616e-4e46-baa3-464767b01716"));

            migrationBuilder.AddColumn<Guid>(
                name: "StoreBillId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15c20fc4-b4f5-476b-8d38-2ab7139b7500"), "Chef" },
                    { new Guid("52005799-3de1-4b83-9f57-19179e01da35"), "Cashier" },
                    { new Guid("791aeba2-bd10-48f1-829f-6c86002b6ebb"), "Waiter" },
                    { new Guid("cb537f9a-63c4-476c-bdcb-640c66ac335f"), "Admin" }
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
                principalColumn: "Id");
        }
    }
}
