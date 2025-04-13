using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemInfo_Name",
                table: "StockItems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemInfo_Sku",
                table: "StockItems",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "ItemInfo_Name",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "ItemInfo_Sku",
                table: "StockItems");
        }
    }
}
