using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class alot_stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoldPrice",
                table: "QrCodes",
                newName: "SellerCommissionFee");

            migrationBuilder.AlterColumn<float>(
                name: "CommissionRate",
                table: "Vouchers",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "DefaultCommissionRate",
                table: "ServiceTypes",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "Sellers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CommissionFee",
                table: "QrCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProviderRevenue",
                table: "QrCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "QrCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CommissionFee",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProviderRevenue",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellerCommission",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SellerRank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionRatePercent = table.Column<float>(type: "real", nullable: false),
                    EpxRequired = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerRank", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_RankId",
                table: "Sellers",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_SellerRank_RankId",
                table: "Sellers",
                column: "RankId",
                principalTable: "SellerRank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_SellerRank_RankId",
                table: "Sellers");

            migrationBuilder.DropTable(
                name: "SellerRank");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_RankId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "CommissionFee",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "ProviderRevenue",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "CommissionFee",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProviderRevenue",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SellerCommission",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "SellerCommissionFee",
                table: "QrCodes",
                newName: "SoldPrice");

            migrationBuilder.AlterColumn<long>(
                name: "CommissionRate",
                table: "Vouchers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "DefaultCommissionRate",
                table: "ServiceTypes",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
