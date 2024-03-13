using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace align.Migrations
{
    /// <inheritdoc />
    public partial class AssignHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAssignHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RegionManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAssignHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAssignHistory_AssignerUser",
                        column: x => x.AssignerUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAssignHistory_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAssignHistory_RegionManager",
                        column: x => x.RegionManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssignHistory_AssignerUserId",
                table: "ProductAssignHistory",
                column: "AssignerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssignHistory_ProductId",
                table: "ProductAssignHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssignHistory_RegionManagerId",
                table: "ProductAssignHistory",
                column: "RegionManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAssignHistory");
        }
    }
}
