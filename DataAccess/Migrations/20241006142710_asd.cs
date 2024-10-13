using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4155b23e-5e10-49aa-9c55-94dc95e8f133"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("5e2911ac-a4c9-442f-9c28-fa15dc293851"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("97935855-bfc0-46b7-bf33-9687a7ec4f18"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b98ed727-6085-4acc-b819-754a759cea57"));

            migrationBuilder.AddColumn<Guid>(
                name: "StoreBillId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.DropColumn(
                name: "StoreBillId",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4155b23e-5e10-49aa-9c55-94dc95e8f133"), "Admin" },
                    { new Guid("5e2911ac-a4c9-442f-9c28-fa15dc293851"), "Chef" },
                    { new Guid("97935855-bfc0-46b7-bf33-9687a7ec4f18"), "Cashier" },
                    { new Guid("b98ed727-6085-4acc-b819-754a759cea57"), "Waiter" }
                });
        }
    }
}
