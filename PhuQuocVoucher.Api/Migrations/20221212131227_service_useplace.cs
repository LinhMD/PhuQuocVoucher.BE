using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class service_useplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsePlace",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_OrderId",
                table: "QrCodes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SellerId",
                table: "OrderItems",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ServiceTypeId",
                table: "Activities",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ServiceTypes_ServiceTypeId",
                table: "Activities",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Sellers_SellerId",
                table: "OrderItems",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_Orders_OrderId",
                table: "QrCodes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ServiceTypes_ServiceTypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Sellers_SellerId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_Orders_OrderId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_OrderId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SellerId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ServiceTypeId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UsePlace",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Activities");
        }
    }
}
