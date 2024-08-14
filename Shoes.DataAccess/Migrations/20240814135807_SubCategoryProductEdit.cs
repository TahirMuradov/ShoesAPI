using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubCategoryProductEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Product_ProductId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_ProductId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SubCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "SubCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_ProductId",
                table: "SubCategories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Product_ProductId",
                table: "SubCategories",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
