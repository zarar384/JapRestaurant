using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jap.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class testupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://unsplash.com/photos/brown-rice-on-white-and-blue-ceramic-bowl-xBMNxrbQonw");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://unsplash.com/photos/three-white-dimsum-on-brown-bowl-D-vDQMTfAAU");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://unsplash.com/photos/raw-meat-on-black-ceramic-plate-PAxbMmoKsF0");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://unsplash.com/photos/sliced-meat-with-green-leaf-on-black-ceramic-plate-e29ha_BbOcQ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://netmastery.blob.core.windows.net/jap/1.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://netmastery.blob.core.windows.net/jap/2.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://netmastery.blob.core.windows.net/jap/3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://netmastery.blob.core.windows.net/jap/1.png");
        }
    }
}
