using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoes.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShippingMethodEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMethodLanguages_ShippingMethods_DeliveryMethodId",
                table: "ShippingMethodLanguages");

            migrationBuilder.RenameColumn(
                name: "DeliveryMethodId",
                table: "ShippingMethodLanguages",
                newName: "ShippingMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingMethodLanguages_DeliveryMethodId",
                table: "ShippingMethodLanguages",
                newName: "IX_ShippingMethodLanguages_ShippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMethodLanguages_ShippingMethods_ShippingMethodId",
                table: "ShippingMethodLanguages",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMethodLanguages_ShippingMethods_ShippingMethodId",
                table: "ShippingMethodLanguages");

            migrationBuilder.RenameColumn(
                name: "ShippingMethodId",
                table: "ShippingMethodLanguages",
                newName: "DeliveryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingMethodLanguages_ShippingMethodId",
                table: "ShippingMethodLanguages",
                newName: "IX_ShippingMethodLanguages_DeliveryMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMethodLanguages_ShippingMethods_DeliveryMethodId",
                table: "ShippingMethodLanguages",
                column: "DeliveryMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
