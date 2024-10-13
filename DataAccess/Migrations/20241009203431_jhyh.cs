using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jhyh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("374aa369-6a2a-4724-ad7a-dacd99b8cc98"), "Admin" },
                    { new Guid("bf207f74-8565-47ee-8b01-fac848b4440d"), "Waiter" },
                    { new Guid("d1121783-6353-4803-86f1-69f021337123"), "Chef" },
                    { new Guid("ef683efb-8bb8-4d4d-a3e9-a2e22adb32ac"), "Cashier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("374aa369-6a2a-4724-ad7a-dacd99b8cc98"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("bf207f74-8565-47ee-8b01-fac848b4440d"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("d1121783-6353-4803-86f1-69f021337123"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ef683efb-8bb8-4d4d-a3e9-a2e22adb32ac"));

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
    }
}
