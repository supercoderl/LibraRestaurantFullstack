using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    DiscountTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CounponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinOrderValue = table.Column<decimal>(type: "money", nullable: false),
                    MinItemQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxDiscountValue = table.Column<decimal>(type: "money", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.DiscountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discount_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_DiscountType_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "DiscountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discount_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MenuItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 17, 16, 56, 13, 492, DateTimeKind.Local).AddTicks(2347));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CategoryId",
                table: "Discounts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ItemId",
                table: "Discounts",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 13, 13, 2, 41, 762, DateTimeKind.Local).AddTicks(5561));
        }
    }
}
