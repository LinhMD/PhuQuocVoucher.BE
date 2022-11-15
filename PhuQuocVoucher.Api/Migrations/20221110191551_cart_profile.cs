using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class cart_profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProfileId",
                table: "CartItems",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Profiles_ProfileId",
                table: "CartItems",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Profiles_ProfileId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProfileId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "CartItems");
        }
    }
}
