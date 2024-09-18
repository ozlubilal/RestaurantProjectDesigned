using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductDescriptionColumn2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("6b1ca21d-0f64-44b6-863c-2a1c4366eb27"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d43be23f-8152-474b-b56a-49eefead817f"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("dec2e7c4-7028-489a-9502-7248b015a9c5"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("f78f7d36-58b8-4b76-8559-8f4422f3423d"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("51346f2d-8967-4a28-a5c2-838e958c954d"), "Admin" },
                    { new Guid("84f6ce6e-0ea5-4398-bdce-32976ef78fda"), "Cashier" },
                    { new Guid("aadadadc-b1db-4328-aaaf-e868bb0ba122"), "Waiter" },
                    { new Guid("e3b6a143-0ce2-49b3-8664-5d3b5fb6ee26"), "Chef" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("51346f2d-8967-4a28-a5c2-838e958c954d"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("84f6ce6e-0ea5-4398-bdce-32976ef78fda"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("aadadadc-b1db-4328-aaaf-e868bb0ba122"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("e3b6a143-0ce2-49b3-8664-5d3b5fb6ee26"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b1ca21d-0f64-44b6-863c-2a1c4366eb27"), "Cashier" },
                    { new Guid("d43be23f-8152-474b-b56a-49eefead817f"), "Chef" },
                    { new Guid("dec2e7c4-7028-489a-9502-7248b015a9c5"), "Admin" },
                    { new Guid("f78f7d36-58b8-4b76-8559-8f4422f3423d"), "Waiter" }
                });
        }
    }
}
