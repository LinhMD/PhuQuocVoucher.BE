using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class more_stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Services");

            migrationBuilder.AddColumn<long>(
                name: "CommissionRate",
                table: "Vouchers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SocialPost",
                table: "Vouchers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "SocialPost",
                table: "Vouchers");

            migrationBuilder.AddColumn<double>(
                name: "CommissionRate",
                table: "Services",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
