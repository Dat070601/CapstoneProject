using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class Dbfixrelationshipstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Orders_OrderId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_OrderId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Statuses");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StatusId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_OrderId",
                table: "Statuses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Orders_OrderId",
                table: "Statuses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
