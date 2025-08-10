using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerIdInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Baskets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Baskets");
        }
    }
}
