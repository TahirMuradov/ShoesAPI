using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DbsetAddedForProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Product_ProductId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLanguage_Product_ProductId",
                table: "ProductLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_SizeProducts_Product_ProductId",
                table: "SizeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Product_ProductId",
                table: "SoldProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryProducts_Product_ProductId",
                table: "SubCategoryProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLanguage",
                table: "ProductLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductLanguage",
                newName: "ProductLanguages");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductLanguage_ProductId",
                table: "ProductLanguages",
                newName: "IX_ProductLanguages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLanguages",
                table: "ProductLanguages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SizeProducts_Products_ProductId",
                table: "SizeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Products_ProductId",
                table: "SoldProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryProducts_Products_ProductId",
                table: "SubCategoryProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLanguages_Products_ProductId",
                table: "ProductLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_SizeProducts_Products_ProductId",
                table: "SizeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Products_ProductId",
                table: "SoldProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryProducts_Products_ProductId",
                table: "SubCategoryProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLanguages",
                table: "ProductLanguages");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductLanguages",
                newName: "ProductLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_ProductLanguages_ProductId",
                table: "ProductLanguage",
                newName: "IX_ProductLanguage_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLanguage",
                table: "ProductLanguage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Product_ProductId",
                table: "Pictures",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLanguage_Product_ProductId",
                table: "ProductLanguage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SizeProducts_Product_ProductId",
                table: "SizeProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Product_ProductId",
                table: "SoldProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryProducts_Product_ProductId",
                table: "SubCategoryProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
