using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class update_somethings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Combos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ProductId",
                table: "Vouchers",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Combos_ProductId",
                table: "Combos",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Combos_Products_ProductId",
                table: "Combos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Products_ProductId",
                table: "Vouchers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combos_Products_ProductId",
                table: "Combos");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Products_ProductId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ProductId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Combos_ProductId",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Combos");
        }
    }
}
