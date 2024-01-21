using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacadePattern.API.Migrations
{
    /// <inheritdoc />
    public partial class CouponDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "descount_porcentage",
                table: "Coupons",
                newName: "discount_porcentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discount_porcentage",
                table: "Coupons",
                newName: "descount_porcentage");
        }
    }
}
