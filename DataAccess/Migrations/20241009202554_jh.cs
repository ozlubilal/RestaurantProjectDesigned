using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("03351ada-31bf-4119-b97f-4d2b12e66280"), "Chef" },
                    { new Guid("4e6d7daf-6581-4e95-9ca2-e29071bbff07"), "Cashier" },
                    { new Guid("9f14425c-ea16-4b7f-923d-b127b679aea0"), "Waiter" },
                    { new Guid("f54192fa-6a9e-4d3e-addc-9d4cd04e41f4"), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
