using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubCategoryAddedIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryCupon_Cupons_CuponId",
                table: "SubCategoryCupon");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryCupon_SubCategories_SubCategoryId",
                table: "SubCategoryCupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategoryCupon",
                table: "SubCategoryCupon");

            migrationBuilder.RenameTable(
                name: "SubCategoryCupon",
                newName: "SubCategoryCupons");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryCupon_SubCategoryId",
                table: "SubCategoryCupons",
                newName: "IX_SubCategoryCupons_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryCupon_CuponId",
                table: "SubCategoryCupons",
                newName: "IX_SubCategoryCupons_CuponId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubCategoryCupons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategoryCupons",
                table: "SubCategoryCupons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryCupons_Cupons_CuponId",
                table: "SubCategoryCupons",
                column: "CuponId",
                principalTable: "Cupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryCupons_SubCategories_SubCategoryId",
                table: "SubCategoryCupons",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryCupons_Cupons_CuponId",
                table: "SubCategoryCupons");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryCupons_SubCategories_SubCategoryId",
                table: "SubCategoryCupons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategoryCupons",
                table: "SubCategoryCupons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubCategoryCupons");

            migrationBuilder.RenameTable(
                name: "SubCategoryCupons",
                newName: "SubCategoryCupon");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryCupons_SubCategoryId",
                table: "SubCategoryCupon",
                newName: "IX_SubCategoryCupon_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryCupons_CuponId",
                table: "SubCategoryCupon",
                newName: "IX_SubCategoryCupon_CuponId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategoryCupon",
                table: "SubCategoryCupon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryCupon_Cupons_CuponId",
                table: "SubCategoryCupon",
                column: "CuponId",
                principalTable: "Cupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryCupon_SubCategories_SubCategoryId",
                table: "SubCategoryCupon",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
