using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    CategoryItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => x.CategoryItemId);
                    table.ForeignKey(
                        name: "FK_CategoryItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryItem_Item_ItemId",
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
                value: new DateTime(2024, 9, 9, 15, 18, 13, 6, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_ItemId",
                table: "CategoryItems",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 31, 9, 8, 15, 216, DateTimeKind.Local).AddTicks(6417));
        }
    }
}
