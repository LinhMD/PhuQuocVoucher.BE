using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class add_PaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentDetail_PaymentDetailId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentDetailId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentDetailId",
                table: "Orders");
        }
    }
}
