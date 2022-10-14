using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Controller.Migrations
{
    public partial class remove_voucher_Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER LC_TRIGGER_AFTER_INSERT_ORDERITEM;");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Vouchers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("CREATE TRIGGER LC_TRIGGER_AFTER_INSERT_ORDERITEM ON OrderItems AFTER Insert AS\r\nBEGIN\r\n  DECLARE @NewOrderProductId INT\r\n  DECLARE InsertedOrderItemCursor CURSOR FOR SELECT OrderProductId FROM Inserted\r\n  OPEN InsertedOrderItemCursor\r\n  FETCH NEXT FROM InsertedOrderItemCursor INTO @NewOrderProductId\r\n  WHILE @@FETCH_STATUS = 0\r\n  BEGIN\r\n    UPDATE Vouchers\r\n    SET Inventory = Vouchers.Inventory - 1\r\n    WHERE Vouchers.ProductId = @NewOrderProductId AND Vouchers.Inventory > 0;\r\n    FETCH NEXT FROM InsertedOrderItemCursor INTO @NewOrderProductId\r\n  END\r\n  CLOSE InsertedOrderItemCursor DEALLOCATE InsertedOrderItemCursor\r\nEND");
        }
    }
}
