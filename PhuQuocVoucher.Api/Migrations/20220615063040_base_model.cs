using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class base_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Vouchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "ServiceTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "ServiceTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ServiceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "ServiceTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Sellers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Sellers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Sellers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "ProviderTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "ProviderTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProviderTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "ProviderTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Providers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Providers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Providers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Profiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Profiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Profiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Places",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Places",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Places",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "PaymentDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "PaymentDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PaymentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "PaymentDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Combos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Combos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Combos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "ProviderTypes");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "ProviderTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProviderTypes");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "ProviderTypes");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Blogs");
        }
    }
}
