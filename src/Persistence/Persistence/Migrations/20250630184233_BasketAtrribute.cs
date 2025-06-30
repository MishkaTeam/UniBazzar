using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BasketAtrribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "BasketItemAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BasketItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductAttributeName = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ProductAttributeValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductAttributeValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_BasketItemAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItemAttributes_BasketItems_BasketItemId",
                        column: x => x.BasketItemId,
                        principalTable: "BasketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemAttributes_BasketItemId",
                table: "BasketItemAttributes",
                column: "BasketItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItemAttributes");
        }
    }
}
