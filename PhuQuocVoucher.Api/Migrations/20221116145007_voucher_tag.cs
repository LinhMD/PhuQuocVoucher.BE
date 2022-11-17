using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class voucher_tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagVoucher_Tags_TagsId",
                table: "TagVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_TagVoucher_Vouchers_VouchersId",
                table: "TagVoucher");

            migrationBuilder.RenameColumn(
                name: "VouchersId",
                table: "TagVoucher",
                newName: "VoucherId");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "TagVoucher",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_TagVoucher_VouchersId",
                table: "TagVoucher",
                newName: "IX_TagVoucher_VoucherId");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_TagId",
                table: "Vouchers",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagVoucher_Tags_TagId",
                table: "TagVoucher",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagVoucher_Vouchers_VoucherId",
                table: "TagVoucher",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Tags_TagId",
                table: "Vouchers",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagVoucher_Tags_TagId",
                table: "TagVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_TagVoucher_Vouchers_VoucherId",
                table: "TagVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Tags_TagId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_TagId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Vouchers");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "TagVoucher",
                newName: "VouchersId");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "TagVoucher",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_TagVoucher_VoucherId",
                table: "TagVoucher",
                newName: "IX_TagVoucher_VouchersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagVoucher_Tags_TagsId",
                table: "TagVoucher",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagVoucher_Vouchers_VouchersId",
                table: "TagVoucher",
                column: "VouchersId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
