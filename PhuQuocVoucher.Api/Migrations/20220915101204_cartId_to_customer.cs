using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class cartId_to_customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Services_ServiceId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsForKid",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Services_ServiceId",
                table: "Vouchers",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Services_ServiceId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Customers");

            migrationBuilder.AddColumn<bool>(
                name: "IsForKid",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Services_ServiceId",
                table: "Vouchers",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
