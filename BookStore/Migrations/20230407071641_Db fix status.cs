using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class Dbfixstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Orders_StatusId",
                table: "Statuses");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Statuses",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_StatusId",
                table: "Statuses",
                newName: "IX_Statuses_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Orders_OrderId",
                table: "Statuses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Orders_OrderId",
                table: "Statuses");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Statuses",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_OrderId",
                table: "Statuses",
                newName: "IX_Statuses_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Orders_StatusId",
                table: "Statuses",
                column: "StatusId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
