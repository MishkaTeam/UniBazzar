using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemAttributesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Platform",
                table: "Orders",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscountAmount",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "TotalDiscountType",
                table: "Orders",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "OrderItemAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BasketItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductAttributeName = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ProductAttributeValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductAttributeValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ordering = table.Column<int>(type: "integer", nullable: false, defaultValue: 10000),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertDateTime = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDateTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemAttributes_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAttributes_OrderItemId",
                table: "OrderItemAttributes",
                column: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemAttributes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalDiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalDiscountType",
                table: "Orders");
        }
    }
}
