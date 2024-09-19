using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ClosedDate",
                table: "StoreBills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2acfe2f8-380c-452e-aca5-4dcddf80d64e"), "Chef" },
                    { new Guid("48cfe384-68d4-4ef3-9666-9808d1dfb8dc"), "Admin" },
                    { new Guid("9c33e5d0-414c-4794-a416-816c4aadb402"), "Waiter" },
                    { new Guid("fed14103-14ff-4b4d-a17d-6300d603e173"), "Cashier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2acfe2f8-380c-452e-aca5-4dcddf80d64e"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("48cfe384-68d4-4ef3-9666-9808d1dfb8dc"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9c33e5d0-414c-4794-a416-816c4aadb402"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("fed14103-14ff-4b4d-a17d-6300d603e173"));

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "StoreBills");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Bills");

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
    }
}
