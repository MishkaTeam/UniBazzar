using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBasketDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlatForm",
                table: "Baskets",
                newName: "Platform");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Baskets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscountAmount",
                table: "Baskets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "TotalDiscountType",
                table: "Baskets",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "TotalDiscountAmount",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "TotalDiscountType",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "Platform",
                table: "Baskets",
                newName: "PlatForm");
        }
    }
}
