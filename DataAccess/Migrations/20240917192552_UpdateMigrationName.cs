using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BillDate",
                table: "StoreBills");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("266d71fa-c2f7-4cb6-be31-09dd2524ab48"), "Waiter" },
                    { new Guid("4d3f128e-86ba-4997-87f6-04e6cc98e2ac"), "Chef" },
                    { new Guid("d2a54c41-a7e7-4e58-a2cb-26d44c1d2b23"), "Admin" },
                    { new Guid("e66b4f9d-92eb-4663-a494-cc0240b2d3f0"), "Cashier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("266d71fa-c2f7-4cb6-be31-09dd2524ab48"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4d3f128e-86ba-4997-87f6-04e6cc98e2ac"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d2a54c41-a7e7-4e58-a2cb-26d44c1d2b23"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("e66b4f9d-92eb-4663-a494-cc0240b2d3f0"));

            migrationBuilder.AddColumn<DateTime>(
                name: "BillDate",
                table: "StoreBills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
