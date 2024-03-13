using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace align.Migrations
{
    /// <inheritdoc />
    public partial class product_user_many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_RegionManager",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_RegionManagerId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "RegionManagerId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductUser",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    RegionManagersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser", x => new { x.ProductsId, x.RegionManagersId });
                    table.ForeignKey(
                        name: "FK_ProductUser_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductUser_User_RegionManagersId",
                        column: x => x.RegionManagersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUser_RegionManagersId",
                table: "ProductUser",
                column: "RegionManagersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductUser");

            migrationBuilder.AddColumn<string>(
                name: "RegionManagerId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_RegionManagerId",
                table: "Product",
                column: "RegionManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_RegionManager",
                table: "Product",
                column: "RegionManagerId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
