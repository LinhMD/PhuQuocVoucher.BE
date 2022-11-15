using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class voucher_1_M_review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_OrderItems_OrderItemId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OrderItemId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "Reviews",
                newName: "VoucherId");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VoucherId",
                table: "Reviews",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ReviewId",
                table: "OrderItems",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Reviews_ReviewId",
                table: "OrderItems",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Vouchers_VoucherId",
                table: "Reviews",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Reviews_ReviewId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Vouchers_VoucherId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_VoucherId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ReviewId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "Reviews",
                newName: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderItemId",
                table: "Reviews",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_OrderItems_OrderItemId",
                table: "Reviews",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
