using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("65923437-3551-43ed-8b95-265fdffc2ba4"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8515151a-0e38-4427-94ad-02e3755bed2d"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a16a6bf9-9d5a-4a03-8ed9-935c0e9fdae9"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ce4aa887-228c-4946-a0f8-66a7ffc2c28f"));

            migrationBuilder.DropColumn(
                name: "StoreBillId",
                table: "Payments");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("65923437-3551-43ed-8b95-265fdffc2ba4"), "Cashier" },
                    { new Guid("8515151a-0e38-4427-94ad-02e3755bed2d"), "Chef" },
                    { new Guid("a16a6bf9-9d5a-4a03-8ed9-935c0e9fdae9"), "Waiter" },
                    { new Guid("ce4aa887-228c-4946-a0f8-66a7ffc2c28f"), "Admin" }
                });
        }
    }
}
