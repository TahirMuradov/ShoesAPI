using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class relatedCuponEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCupons");

            migrationBuilder.DropTable(
                name: "ProductCupons");

            migrationBuilder.DropTable(
                name: "SubCategoryCupons");

            migrationBuilder.DropTable(
                name: "UserCupons");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Cupons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Cupons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId",
                table: "Cupons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Cupons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_CategoryId",
                table: "Cupons",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_ProductId",
                table: "Cupons",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_SubCategoryId",
                table: "Cupons",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cupons_UserId",
                table: "Cupons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_Categories_CategoryId",
                table: "Cupons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_Products_ProductId",
                table: "Cupons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_SubCategories_SubCategoryId",
                table: "Cupons",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupons_Users_UserId",
                table: "Cupons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_Categories_CategoryId",
                table: "Cupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_Products_ProductId",
                table: "Cupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_SubCategories_SubCategoryId",
                table: "Cupons");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupons_Users_UserId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_CategoryId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_ProductId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_SubCategoryId",
                table: "Cupons");

            migrationBuilder.DropIndex(
                name: "IX_Cupons_UserId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Cupons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cupons");

            migrationBuilder.CreateTable(
                name: "CategoryCupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryCupons_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCupons_Cupons_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCupons_Cupons_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCupons_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryCupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryCupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryCupons_Cupons_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoryCupons_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCupons_Cupons_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCupons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCupons_CategoryId",
                table: "CategoryCupons",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCupons_CuponId",
                table: "CategoryCupons",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCupons_CuponId",
                table: "ProductCupons",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCupons_ProductId",
                table: "ProductCupons",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryCupons_CuponId",
                table: "SubCategoryCupons",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryCupons_SubCategoryId",
                table: "SubCategoryCupons",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCupons_CuponId",
                table: "UserCupons",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCupons_UserId",
                table: "UserCupons",
                column: "UserId");
        }
    }
}
