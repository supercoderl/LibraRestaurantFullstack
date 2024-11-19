using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQuantity = table.Column<int>(type: "int", nullable: false),
                    NewQuantity = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_OrderLog_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MenuItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLog_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 13, 13, 2, 41, 762, DateTimeKind.Local).AddTicks(5561));

            migrationBuilder.CreateIndex(
                name: "IX_OrderLogs_ItemId",
                table: "OrderLogs",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLogs_OrderId",
                table: "OrderLogs",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLogs");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 9, 53, 13, 556, DateTimeKind.Local).AddTicks(7839));
        }
    }
}
