using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("03351ada-31bf-4119-b97f-4d2b12e66280"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4e6d7daf-6581-4e95-9ca2-e29071bbff07"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9f14425c-ea16-4b7f-923d-b127b679aea0"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("f54192fa-6a9e-4d3e-addc-9d4cd04e41f4"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("03351ada-31bf-4119-b97f-4d2b12e66280"), "Chef" },
                    { new Guid("4e6d7daf-6581-4e95-9ca2-e29071bbff07"), "Cashier" },
                    { new Guid("9f14425c-ea16-4b7f-923d-b127b679aea0"), "Waiter" },
                    { new Guid("f54192fa-6a9e-4d3e-addc-9d4cd04e41f4"), "Admin" }
                });
        }
    }
}
