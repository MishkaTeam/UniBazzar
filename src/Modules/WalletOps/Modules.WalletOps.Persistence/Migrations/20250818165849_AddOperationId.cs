using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.WalletOps.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperationId",
                schema: "WLT",
                table: "Transactions",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperationId",
                schema: "WLT",
                table: "HeldFunds",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OperationId",
                schema: "WLT",
                table: "Transactions",
                column: "OperationId",
                unique: true,
                filter: "\"OperationId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HeldFunds_OperationId",
                schema: "WLT",
                table: "HeldFunds",
                column: "OperationId",
                unique: true,
                filter: "\"OperationId\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_OperationId",
                schema: "WLT",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_HeldFunds_OperationId",
                schema: "WLT",
                table: "HeldFunds");

            migrationBuilder.DropColumn(
                name: "OperationId",
                schema: "WLT",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OperationId",
                schema: "WLT",
                table: "HeldFunds");
        }
    }
}
