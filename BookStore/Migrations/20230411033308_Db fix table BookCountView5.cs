using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class DbfixtableBookCountView5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Shops_ShopId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Shops_ShopId",
                table: "Images",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Shops_ShopId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Shops_ShopId",
                table: "Images",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
