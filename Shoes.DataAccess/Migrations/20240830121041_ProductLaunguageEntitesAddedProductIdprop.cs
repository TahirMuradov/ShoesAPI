using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductLaunguageEntitesAddedProductIdprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductLanguages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
