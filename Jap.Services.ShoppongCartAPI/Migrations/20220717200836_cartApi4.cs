using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jap.Services.ShoppingCartAPI.Migrations
{
    public partial class cartApi4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "CartHeaders");
        }
    }
}
