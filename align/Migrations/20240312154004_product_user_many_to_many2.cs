using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace align.Migrations
{
    /// <inheritdoc />
    public partial class product_user_many_to_many2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedProductAmount",
                table: "ProductAssignHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedProductAmount",
                table: "ProductAssignHistory");
        }
    }
}
