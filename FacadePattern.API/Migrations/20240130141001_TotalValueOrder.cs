using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacadePattern.API.Migrations
{
    /// <inheritdoc />
    public partial class TotalValueOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_value",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_value",
                table: "Orders");
        }
    }
}
