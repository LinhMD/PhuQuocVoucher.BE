using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class field_serviceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DefaultCommissionRate",
                table: "ServiceTypes",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCommissionRate",
                table: "ServiceTypes");
        }
    }
}
