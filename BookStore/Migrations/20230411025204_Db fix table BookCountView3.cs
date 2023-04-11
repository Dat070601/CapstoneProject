using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class DbfixtableBookCountView3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accounts_AccountId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AccountId",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccountId",
                table: "Images",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accounts_AccountId",
                table: "Images",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Accounts_AccountId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AccountId",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccountId",
                table: "Images",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Accounts_AccountId",
                table: "Images",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
