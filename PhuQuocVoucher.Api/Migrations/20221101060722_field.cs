using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotNumber",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CommissionRate",
                table: "Services",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentRequestId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotNumber",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PaymentDetailId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentRequestId",
                table: "Orders");
        }
    }
}
