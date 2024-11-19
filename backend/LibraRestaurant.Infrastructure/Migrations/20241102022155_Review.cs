using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikeCounts = table.Column<int>(type: "int", nullable: false),
                    IsVerifiedPurchase = table.Column<bool>(type: "bit", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Item_ItemId",
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
                value: new DateTime(2024, 11, 2, 9, 21, 53, 210, DateTimeKind.Local).AddTicks(2826));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 28, 11, 25, 37, 971, DateTimeKind.Local).AddTicks(98));
        }
    }
}
