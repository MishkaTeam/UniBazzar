using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PriceList_002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_ProductPriceLists_PriceListId",
                table: "PriceListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem");

            migrationBuilder.DropIndex(
                name: "IX_PriceListItem_PriceListId",
                table: "PriceListItem");

            migrationBuilder.AlterColumn<int>(
                name: "Ordering",
                table: "ProductPriceLists",
                type: "integer",
                nullable: false,
                defaultValue: 10000,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "PriceListId",
                table: "PriceListItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "PriceListItem",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem",
                columns: new[] { "PriceListId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_ProductPriceLists_PriceListId",
                table: "PriceListItem",
                column: "PriceListId",
                principalTable: "ProductPriceLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListItem_ProductPriceLists_PriceListId",
                table: "PriceListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem");

            migrationBuilder.AlterColumn<int>(
                name: "Ordering",
                table: "ProductPriceLists",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 10000);

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "PriceListItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "PriceListId",
                table: "PriceListItem",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceListItem",
                table: "PriceListItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_PriceListId",
                table: "PriceListItem",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListItem_ProductPriceLists_PriceListId",
                table: "PriceListItem",
                column: "PriceListId",
                principalTable: "ProductPriceLists",
                principalColumn: "Id");
        }
    }
}
