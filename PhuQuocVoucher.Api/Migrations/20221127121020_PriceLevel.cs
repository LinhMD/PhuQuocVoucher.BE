using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class PriceLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "PriceBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdult = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<bool>(type: "bit", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceBooks_LevelId",
                table: "PriceBooks",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceBooks_PriceLevels_LevelId",
                table: "PriceBooks",
                column: "LevelId",
                principalTable: "PriceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceBooks_PriceLevels_LevelId",
                table: "PriceBooks");

            migrationBuilder.DropTable(
                name: "PriceLevels");

            migrationBuilder.DropIndex(
                name: "IX_PriceBooks_LevelId",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "PriceBooks");
        }
    }
}
