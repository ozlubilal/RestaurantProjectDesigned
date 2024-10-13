using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class nb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("07206f20-3f62-4c7d-aa56-6f4951bd2c82"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("50a236f0-de19-4f14-8e52-5265c7d25ab5"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("726afd8b-59a6-47ff-978b-3fc2ef1520d7"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8c3e7ce2-3833-429a-b125-c1c23f939d15"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("819d02fc-d19a-487a-ade8-ceb59dcff492"), "Waiter" },
                    { new Guid("965b0927-96f1-4328-b919-622a9a0d16c3"), "Admin" },
                    { new Guid("a1b30566-2844-412c-97ac-ae95f7bb076c"), "Chef" },
                    { new Guid("aa69af8c-1bf2-4f38-b109-f27067bd3080"), "Cashier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("819d02fc-d19a-487a-ade8-ceb59dcff492"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("965b0927-96f1-4328-b919-622a9a0d16c3"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a1b30566-2844-412c-97ac-ae95f7bb076c"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("aa69af8c-1bf2-4f38-b109-f27067bd3080"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("07206f20-3f62-4c7d-aa56-6f4951bd2c82"), "Admin" },
                    { new Guid("50a236f0-de19-4f14-8e52-5265c7d25ab5"), "Chef" },
                    { new Guid("726afd8b-59a6-47ff-978b-3fc2ef1520d7"), "Cashier" },
                    { new Guid("8c3e7ce2-3833-429a-b125-c1c23f939d15"), "Waiter" }
                });
        }
    }
}
