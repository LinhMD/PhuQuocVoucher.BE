using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class serviceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "QrCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_ServiceTypeId",
                table: "QrCodes",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_ServiceTypes_ServiceTypeId",
                table: "QrCodes",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_ServiceTypes_ServiceTypeId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_ServiceTypeId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "QrCodes");
        }
    }
}
