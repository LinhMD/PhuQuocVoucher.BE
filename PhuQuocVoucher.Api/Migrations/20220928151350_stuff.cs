using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSellerPrice",
                table: "PriceLevels");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PriceBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSellerPrice",
                table: "PriceBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "IsSellerPrice",
                table: "PriceBooks");

            migrationBuilder.AddColumn<bool>(
                name: "IsSellerPrice",
                table: "PriceLevels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
