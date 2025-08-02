using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "ProductTotalPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductTotalPrice",
                table: "BasketItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HomeViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Sorting = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemic = table.Column<bool>(type: "boolean", nullable: false),
                    DeactivateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    table.PrimaryKey("PK_HomeViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageViewItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NavigationUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Column = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    HomeViewId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ImageViewItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageViewItems_HomeViews_HomeViewId",
                        column: x => x.HomeViewId,
                        principalTable: "HomeViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductViewItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    HomeViewId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ProductViewItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductViewItems_HomeViews_HomeViewId",
                        column: x => x.HomeViewId,
                        principalTable: "HomeViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductViewItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SliderViewItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NavigationUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Interval = table.Column<int>(type: "integer", nullable: false),
                    HomeViewId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_SliderViewItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SliderViewItems_HomeViews_HomeViewId",
                        column: x => x.HomeViewId,
                        principalTable: "HomeViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageViewItems_HomeViewId",
                table: "ImageViewItems",
                column: "HomeViewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewItems_HomeViewId",
                table: "ProductViewItems",
                column: "HomeViewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewItems_ProductId",
                table: "ProductViewItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SliderViewItems_HomeViewId",
                table: "SliderViewItems",
                column: "HomeViewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageViewItems");

            migrationBuilder.DropTable(
                name: "ProductViewItems");

            migrationBuilder.DropTable(
                name: "SliderViewItems");

            migrationBuilder.DropTable(
                name: "HomeViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValues");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductTotalPrice",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductTotalPrice",
                table: "BasketItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues",
                columns: new[] { "AttributeId", "Id" });
        }
    }
}
