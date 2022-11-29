using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class remove_name_index_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "QrCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_ProviderId",
                table: "QrCodes",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_Providers_ProviderId",
                table: "QrCodes",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_Providers_ProviderId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_ProviderId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "QrCodes");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }
    }
}
