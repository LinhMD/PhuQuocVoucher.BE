using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhuQuocVoucher.Api.Migrations
{
    public partial class fix_blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPlace_Blogs_BlogsId",
                table: "BlogPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPlace_Places_PlacesId",
                table: "BlogPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_TagsId",
                table: "BlogTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "BlogTag",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BlogsId",
                table: "BlogTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "PlacesId",
                table: "BlogPlace",
                newName: "PlaceId");

            migrationBuilder.RenameColumn(
                name: "BlogsId",
                table: "BlogPlace",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPlace_PlacesId",
                table: "BlogPlace",
                newName: "IX_BlogPlace_PlaceId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "QrCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "QrCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "QrCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "PriceBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "PriceBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PriceBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "PriceBooks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogTag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                columns: new[] { "BlogId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_BlogId",
                table: "BlogTag",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PlaceId",
                table: "Blogs",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TagId",
                table: "Blogs",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPlace_BlogId",
                table: "BlogPlace",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPlace_Blogs_BlogId",
                table: "BlogPlace",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPlace_Places_PlaceId",
                table: "BlogPlace",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Places_PlaceId",
                table: "Blogs",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Tags_TagId",
                table: "Blogs",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogId",
                table: "BlogTag",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPlace_Blogs_BlogId",
                table: "BlogPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPlace_Places_PlaceId",
                table: "BlogPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Places_PlaceId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Tags_TagId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_BlogId",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_PlaceId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_TagId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_BlogPlace_BlogId",
                table: "BlogPlace");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "PriceBooks");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogTag");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BlogTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "BlogTag",
                newName: "BlogsId");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "BlogPlace",
                newName: "PlacesId");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "BlogPlace",
                newName: "BlogsId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPlace_PlaceId",
                table: "BlogPlace",
                newName: "IX_BlogPlace_PlacesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                columns: new[] { "BlogsId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagsId",
                table: "BlogTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPlace_Blogs_BlogsId",
                table: "BlogPlace",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPlace_Places_PlacesId",
                table: "BlogPlace",
                column: "PlacesId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
