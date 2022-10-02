using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class qr_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNeedProviderConfirm",
                table: "Vouchers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QrCodeId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QrCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QrCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QrCodes_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_QrCodeId",
                table: "OrderItems",
                column: "QrCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_HashCode",
                table: "QrCodes",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_VoucherId",
                table: "QrCodes",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_QrCodes_QrCodeId",
                table: "OrderItems",
                column: "QrCodeId",
                principalTable: "QrCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_QrCodes_QrCodeId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_QrCodeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "IsNeedProviderConfirm",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "QrCodeId",
                table: "OrderItems");
        }
    }
}
