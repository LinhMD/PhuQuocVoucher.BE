using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_TypeId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Services",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_TypeId",
                table: "Services",
                newName: "IX_Services_ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTypes_ServiceTypeId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "Services",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                newName: "IX_Services_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTypes_TypeId",
                table: "Services",
                column: "TypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
