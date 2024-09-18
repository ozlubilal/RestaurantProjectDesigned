using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("34107dae-1d57-41a2-aed4-fbd00daf2adb"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c2bf26b1-ef37-43ef-a781-cbabd0804387"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("c5b0a50a-9f1e-4367-a0ac-031317547947"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ca7bea0d-c7f2-4137-9912-58b669418ac1"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("34107dae-1d57-41a2-aed4-fbd00daf2adb"), "Chef" },
                    { new Guid("c2bf26b1-ef37-43ef-a781-cbabd0804387"), "Admin" },
                    { new Guid("c5b0a50a-9f1e-4367-a0ac-031317547947"), "Waiter" },
                    { new Guid("ca7bea0d-c7f2-4137-9912-58b669418ac1"), "Cashier" }
                });
        }
    }
}
