using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class paynent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

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
    }
}
