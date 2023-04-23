using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Orders",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Addresses",
                newName: "District");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "District",
                table: "Orders",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Addresses",
                newName: "Country");
        }
    }
}
