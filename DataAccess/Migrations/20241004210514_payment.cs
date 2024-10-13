using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedDate",
                table: "StoreBills",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedDate",
                table: "Bills",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("55f6a9c6-69d4-4ea2-9183-e6d29fc53292"), "Cashier" },
                    { new Guid("63d13ac3-e695-4473-b876-f4a801b763dd"), "Admin" },
                    { new Guid("8ff6d920-c4d1-4aa5-8b6e-cf321a8d1481"), "Chef" },
                    { new Guid("b8d0fe77-30d6-43ae-b618-98fcf242989e"), "Waiter" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("55f6a9c6-69d4-4ea2-9183-e6d29fc53292"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("63d13ac3-e695-4473-b876-f4a801b763dd"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8ff6d920-c4d1-4aa5-8b6e-cf321a8d1481"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b8d0fe77-30d6-43ae-b618-98fcf242989e"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedDate",
                table: "StoreBills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosedDate",
                table: "Bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
