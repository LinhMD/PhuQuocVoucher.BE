using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class orderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_Orders_OrderId",
                table: "QrCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Orders_OrderId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_OrderId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_OrderId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Vouchers");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "QrCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SoldPrice = table.Column<long>(type: "bigint", nullable: false),
                    IsCombo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_OrderItemId",
                table: "QrCodes",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProviderId",
                table: "OrderItems",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VoucherId",
                table: "OrderItems",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_OrderItems_OrderItemId",
                table: "QrCodes",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_OrderItems_OrderItemId",
                table: "QrCodes");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_OrderItemId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "QrCodes");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_OrderId",
                table: "Vouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_OrderId",
                table: "QrCodes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_Orders_OrderId",
                table: "QrCodes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Orders_OrderId",
                table: "Vouchers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
