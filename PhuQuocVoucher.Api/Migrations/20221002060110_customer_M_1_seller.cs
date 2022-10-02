using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class customer_M_1_seller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignSellerId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AssignSellerId",
                table: "Customers",
                column: "AssignSellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Sellers_AssignSellerId",
                table: "Customers",
                column: "AssignSellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Sellers_AssignSellerId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AssignSellerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AssignSellerId",
                table: "Customers");
        }
    }
}
