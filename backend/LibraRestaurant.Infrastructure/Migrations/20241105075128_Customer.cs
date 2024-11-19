using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "OrderHeaders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 5, 14, 51, 26, 188, DateTimeKind.Local).AddTicks(9414));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_CustomerId",
                table: "OrderHeaders",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "OrderHeaders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Customer_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "OrderHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Customer_CustomerId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_CustomerId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 2, 9, 21, 53, 210, DateTimeKind.Local).AddTicks(2826));
        }
    }
}
