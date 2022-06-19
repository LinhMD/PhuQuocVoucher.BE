using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class update_PaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentDetail_PaymentDetailId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentDetailId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentDetailId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PaymentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_OrderId",
                table: "PaymentDetail",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_Orders_OrderId",
                table: "PaymentDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_Orders_OrderId",
                table: "PaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetail_OrderId",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PaymentDetail");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentDetailId",
                table: "Orders",
                column: "PaymentDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentDetail_PaymentDetailId",
                table: "Orders",
                column: "PaymentDetailId",
                principalTable: "PaymentDetail",
                principalColumn: "Id");
        }
    }
}
