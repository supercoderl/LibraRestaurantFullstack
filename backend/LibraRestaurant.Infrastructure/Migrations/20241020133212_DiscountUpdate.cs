using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiscountUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DiscountTargetType",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    InvoiceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    CanceledTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 20, 20, 32, 10, 235, DateTimeKind.Local).AddTicks(9161));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_InvoiceId",
                table: "Discounts",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_OrderId",
                table: "Discounts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Invoice_InvoiceId",
                table: "Discounts",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Order_OrderId",
                table: "Discounts",
                column: "OrderId",
                principalTable: "OrderHeaders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Invoice_InvoiceId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Order_OrderId",
                table: "Discounts");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_InvoiceId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_OrderId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountTargetType",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Discounts");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 16, 56, 13, 492, DateTimeKind.Local).AddTicks(2347));
        }
    }
}
