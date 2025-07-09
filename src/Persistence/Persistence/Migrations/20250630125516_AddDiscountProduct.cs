using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AttributeValues_Branches_AttributeId",
            //    table: "AttributeValues");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BasketItems_Orders_BasketId",
            //    table: "BasketItems");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Baskets_Orders_BasketId",
            //    table: "Baskets");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Branches_Categories_CategoryId",
            //    table: "Branches");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderItems_Order_OrderId",
            //    table: "OrderItems");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductFeatures_Branches_AttributeId",
            //    table: "ProductFeatures");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductFeatures_Products_ProductId",
            //    table: "ProductFeatures");

            //migrationBuilder.DropTable(
            //    name: "Order");

            //migrationBuilder.DropIndex(
            //    name: "IX_ProductFeatures_AttributeId",
            //    table: "ProductFeatures");

            //migrationBuilder.DropIndex(
            //    name: "IX_Branches_CategoryId",
            //    table: "Branches");

            //migrationBuilder.DropColumn(
            //    name: "IsPinned",
            //    table: "ProductImages");

            //migrationBuilder.DropColumn(
            //    name: "Key",
            //    table: "ProductImages");

            //migrationBuilder.DropColumn(
            //    name: "Value",
            //    table: "ProductImages");

            //migrationBuilder.DropColumn(
            //    name: "AttributeId",
            //    table: "ProductFeatures");

            //migrationBuilder.DropColumn(
            //    name: "ProductAttributeType",
            //    table: "ProductFeatures");

            //migrationBuilder.DropColumn(
            //    name: "BasketStatus",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "Description",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "Platform",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "CategoryId",
            //    table: "Branches");

            //migrationBuilder.DropColumn(
            //    name: "Description",
            //    table: "Branches");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Orders",
            //    newName: "BasketReferenceNumber");

            //migrationBuilder.RenameColumn(
            //    name: "BasketId",
            //    table: "Baskets",
            //    newName: "Id");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "ProductId",
            //    table: "ProductFeatures",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            //    oldClrType: typeof(Guid),
            //    oldType: "uuid",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPinned",
            //    table: "ProductFeatures",
            //    type: "boolean",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Key",
            //    table: "ProductFeatures",
            //    type: "character varying(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "Value",
            //    table: "ProductFeatures",
            //    type: "character varying(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "BasketId",
            //    table: "Orders",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<byte>(
            //    name: "BasketStatus",
            //    table: "Baskets",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (byte)0);

            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "Baskets",
            //    type: "text",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "InsertDateTime",
            //    table: "Baskets",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "InsertedBy",
            //    table: "Baskets",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<int>(
            //    name: "Ordering",
            //    table: "Baskets",
            //    type: "integer",
            //    nullable: false,
            //    defaultValue: 10000);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "OwnerId",
            //    table: "Baskets",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<byte>(
            //    name: "Platform",
            //    table: "Baskets",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (byte)0);

            //migrationBuilder.AddColumn<string>(
            //    name: "ReferenceNumber",
            //    table: "Baskets",
            //    type: "character varying(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "StoreId",
            //    table: "Baskets",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<long>(
            //    name: "UpdateDateTime",
            //    table: "Baskets",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UpdatedBy",
            //    table: "Baskets",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<int>(
            //    name: "Version",
            //    table: "Baskets",
            //    type: "integer",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AlterColumn<string>(
            //    name: "PrimaryKey",
            //    table: "AuditLogs",
            //    type: "text",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "character varying(100)",
            //    oldMaxLength: 100);

            //migrationBuilder.CreateTable(
            //    name: "Attributes",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            //        Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
            //        CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
            //        Ordering = table.Column<int>(type: "integer", nullable: false, defaultValue: 10000),
            //        Version = table.Column<int>(type: "integer", nullable: false),
            //        StoreId = table.Column<Guid>(type: "uuid", nullable: false),
            //        OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
            //        InsertedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //        UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //        InsertDateTime = table.Column<long>(type: "bigint", nullable: false),
            //        UpdateDateTime = table.Column<long>(type: "bigint", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Attributes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Attributes_Categories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Categories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "DiscountProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Ordering = table.Column<int>(type: "integer", nullable: false),
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
                table.PrimaryKey("PK_DiscountProducts", x => x.Id);
                table.ForeignKey(
                    name: "FK_DiscountProducts_Discounts_DiscountId",
                    column: x => x.DiscountId,
                    principalTable: "Discounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DiscountProducts_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

            //migrationBuilder.CreateTable(
            //    name: "ProductAttributes",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
            //        ProductAttributeType = table.Column<byte>(type: "smallint", nullable: false),
            //        ProductId = table.Column<Guid>(type: "uuid", nullable: true),
            //        Ordering = table.Column<int>(type: "integer", nullable: false, defaultValue: 10000),
            //        Version = table.Column<int>(type: "integer", nullable: false),
            //        StoreId = table.Column<Guid>(type: "uuid", nullable: false),
            //        OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
            //        InsertedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //        UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
            //        InsertDateTime = table.Column<long>(type: "bigint", nullable: false),
            //        UpdateDateTime = table.Column<long>(type: "bigint", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductAttributes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ProductAttributes_Attributes_AttributeId",
            //            column: x => x.AttributeId,
            //            principalTable: "Attributes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ProductAttributes_Products_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Products",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Attributes_CategoryId",
            //    table: "Attributes",
            //    column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProducts_DiscountId",
                table: "DiscountProducts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountProducts_ProductId",
                table: "DiscountProducts",
                column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductAttributes_AttributeId",
            //    table: "ProductAttributes",
            //    column: "AttributeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductAttributes_ProductId",
            //    table: "ProductAttributes",
            //    column: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AttributeValues_Attributes_AttributeId",
            //    table: "AttributeValues",
            //    column: "AttributeId",
            //    principalTable: "Attributes",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BasketItems_Baskets_BasketId",
            //    table: "BasketItems",
            //    column: "BasketId",
            //    principalTable: "Baskets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderItems_Orders_OrderId",
            //    table: "OrderItems",
            //    column: "OrderId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductFeatures_Products_ProductId",
            //    table: "ProductFeatures",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductReviews_Customers_CustomerId",
            //    table: "ProductReviews",
            //    column: "CustomerId",
            //    principalTable: "Customers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductReviews_Products_ProductId",
            //    table: "ProductReviews",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AttributeValues_Attributes_AttributeId",
            //    table: "AttributeValues");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BasketItems_Baskets_BasketId",
            //    table: "BasketItems");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderItems_Orders_OrderId",
            //    table: "OrderItems");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductFeatures_Products_ProductId",
            //    table: "ProductFeatures");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductReviews_Customers_CustomerId",
            //    table: "ProductReviews");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductReviews_Products_ProductId",
            //    table: "ProductReviews");

            migrationBuilder.DropTable(
                name: "DiscountProducts");

            //migrationBuilder.DropTable(
            //    name: "ProductAttributes");

            //migrationBuilder.DropTable(
            //    name: "Attributes");

            //migrationBuilder.DropColumn(
            //    name: "IsPinned",
            //    table: "ProductFeatures");

            //migrationBuilder.DropColumn(
            //    name: "Key",
            //    table: "ProductFeatures");

            //migrationBuilder.DropColumn(
            //    name: "Value",
            //    table: "ProductFeatures");

            //migrationBuilder.DropColumn(
            //    name: "BasketId",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "BasketStatus",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "Description",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "InsertDateTime",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "InsertedBy",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "Ordering",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "OwnerId",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "Platform",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "ReferenceNumber",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "StoreId",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "UpdateDateTime",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "UpdatedBy",
            //    table: "Baskets");

            //migrationBuilder.DropColumn(
            //    name: "Version",
            //    table: "Baskets");

            //migrationBuilder.RenameColumn(
            //    name: "BasketReferenceNumber",
            //    table: "Orders",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "Baskets",
            //    newName: "BasketId");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPinned",
            //    table: "ProductImages",
            //    type: "boolean",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Key",
            //    table: "ProductImages",
            //    type: "character varying(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "Value",
            //    table: "ProductImages",
            //    type: "character varying(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "ProductId",
            //    table: "ProductFeatures",
            //    type: "uuid",
            //    nullable: true,
            //    oldClrType: typeof(Guid),
            //    oldType: "uuid");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "AttributeId",
            //    table: "ProductFeatures",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<byte>(
            //    name: "ProductAttributeType",
            //    table: "ProductFeatures",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (byte)0);

            //migrationBuilder.AddColumn<byte>(
            //    name: "BasketStatus",
            //    table: "Orders",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (byte)0);

            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "Orders",
            //    type: "text",
            //    nullable: true);

            //migrationBuilder.AddColumn<byte>(
            //    name: "Platform",
            //    table: "Orders",
            //    type: "smallint",
            //    nullable: false,
            //    defaultValue: (byte)0);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "CategoryId",
            //    table: "Branches",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "Branches",
            //    type: "character varying(1000)",
            //    maxLength: 1000,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "PrimaryKey",
            //    table: "AuditLogs",
            //    type: "character varying(100)",
            //    maxLength: 100,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "text");

            //migrationBuilder.CreateTable(
            //    name: "Order",
            //    columns: table => new
            //    {
            //        TempId1 = table.Column<Guid>(type: "uuid", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.UniqueConstraint("AK_Order_TempId1", x => x.TempId1);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductFeatures_AttributeId",
            //    table: "ProductFeatures",
            //    column: "AttributeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Branches_CategoryId",
            //    table: "Branches",
            //    column: "CategoryId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AttributeValues_Branches_AttributeId",
            //    table: "AttributeValues",
            //    column: "AttributeId",
            //    principalTable: "Branches",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BasketItems_Orders_BasketId",
            //    table: "BasketItems",
            //    column: "BasketId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Baskets_Orders_BasketId",
            //    table: "Baskets",
            //    column: "BasketId",
            //    principalTable: "Orders",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Branches_Categories_CategoryId",
            //    table: "Branches",
            //    column: "CategoryId",
            //    principalTable: "Categories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderItems_Order_OrderId",
            //    table: "OrderItems",
            //    column: "OrderId",
            //    principalTable: "Order",
            //    principalColumn: "TempId1",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductFeatures_Branches_AttributeId",
            //    table: "ProductFeatures",
            //    column: "AttributeId",
            //    principalTable: "Branches",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductFeatures_Products_ProductId",
            //    table: "ProductFeatures",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id");
        }
    }
}
