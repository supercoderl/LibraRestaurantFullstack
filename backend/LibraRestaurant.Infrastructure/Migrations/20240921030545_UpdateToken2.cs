using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraRestaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToken2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Tokens",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 21, 10, 5, 44, 720, DateTimeKind.Local).AddTicks(4084));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Tokens");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 20, 15, 32, 32, 337, DateTimeKind.Local).AddTicks(6334));
        }
    }
}
