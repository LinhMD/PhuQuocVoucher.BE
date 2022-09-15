using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class price_level : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UseDate",
                table: "OrderItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PriceLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSellerPrice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceLevelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceBook_PriceLevel_PriceLevelId",
                        column: x => x.PriceLevelId,
                        principalTable: "PriceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceBook_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PriceId",
                table: "OrderItems",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PriceId",
                table: "CartItems",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceBook_PriceLevelId",
                table: "PriceBook",
                column: "PriceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceBook_ProductId",
                table: "PriceBook",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PriceBook_PriceId",
                table: "CartItems",
                column: "PriceId",
                principalTable: "PriceBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PriceBook_PriceId",
                table: "OrderItems",
                column: "PriceId",
                principalTable: "PriceBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PriceBook_PriceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PriceBook_PriceId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "PriceBook");

            migrationBuilder.DropTable(
                name: "PriceLevel");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_PriceId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_PriceId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "CartItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UseDate",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
