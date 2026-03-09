using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSellersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { // 1️⃣ FK kaldır
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

            // 2️⃣ Index kaldır
            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                table: "Products");

            // 3️⃣ Kolonu kaldır
            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            // 4️⃣ Seller tablosunu sil
            migrationBuilder.DropTable(
                name: "Sellers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
