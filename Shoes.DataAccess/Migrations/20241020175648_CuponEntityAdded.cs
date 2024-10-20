using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CuponEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisCountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryCupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "SubCategoryCupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryCupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryCupon_Cupons_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoryCupon_SubCategories_SubCategoryId",
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "IX_SubCategoryCupon_CuponId",
                table: "SubCategoryCupon",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryCupon_SubCategoryId",
                table: "SubCategoryCupon",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCupons");

            migrationBuilder.DropTable(
                name: "ProductCupons");

            migrationBuilder.DropTable(
                name: "SubCategoryCupon");

            migrationBuilder.DropTable(
                name: "UserCupons");

            migrationBuilder.DropTable(
                name: "Cupons");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}
