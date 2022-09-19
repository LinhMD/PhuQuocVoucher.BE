using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class price_level_and_price_book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PriceBook_PriceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PriceBook_PriceId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceBook_PriceLevel_PriceLevelId",
                table: "PriceBook");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceBook_Products_ProductId",
                table: "PriceBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceLevel",
                table: "PriceLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceBook",
                table: "PriceBook");

            migrationBuilder.RenameTable(
                name: "PriceLevel",
                newName: "PriceLevels");

            migrationBuilder.RenameTable(
                name: "PriceBook",
                newName: "PriceBooks");

            migrationBuilder.RenameIndex(
                name: "IX_PriceBook_ProductId",
                table: "PriceBooks",
                newName: "IX_PriceBooks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceBook_PriceLevelId",
                table: "PriceBooks",
                newName: "IX_PriceBooks_PriceLevelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceLevels",
                table: "PriceLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceBooks",
                table: "PriceBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PriceBooks_PriceId",
                table: "CartItems",
                column: "PriceId",
                principalTable: "PriceBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PriceBooks_PriceId",
                table: "OrderItems",
                column: "PriceId",
                principalTable: "PriceBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBooks_PriceLevels_PriceLevelId",
                table: "PriceBooks",
                column: "PriceLevelId",
                principalTable: "PriceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBooks_Products_ProductId",
                table: "PriceBooks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_PriceBooks_PriceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PriceBooks_PriceId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceBooks_PriceLevels_PriceLevelId",
                table: "PriceBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceBooks_Products_ProductId",
                table: "PriceBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceLevels",
                table: "PriceLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceBooks",
                table: "PriceBooks");

            migrationBuilder.RenameTable(
                name: "PriceLevels",
                newName: "PriceLevel");

            migrationBuilder.RenameTable(
                name: "PriceBooks",
                newName: "PriceBook");

            migrationBuilder.RenameIndex(
                name: "IX_PriceBooks_ProductId",
                table: "PriceBook",
                newName: "IX_PriceBook_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceBooks_PriceLevelId",
                table: "PriceBook",
                newName: "IX_PriceBook_PriceLevelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompleteDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceLevel",
                table: "PriceLevel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceBook",
                table: "PriceBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_PriceBook_PriceId",
                table: "CartItems",
                column: "PriceId",
                principalTable: "PriceBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PriceBook_PriceId",
                table: "OrderItems",
                column: "PriceId",
                principalTable: "PriceBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBook_PriceLevel_PriceLevelId",
                table: "PriceBook",
                column: "PriceLevelId",
                principalTable: "PriceLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBook_Products_ProductId",
                table: "PriceBook",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
