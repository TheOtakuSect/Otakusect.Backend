using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtakuSect.Data.Migrations
{
    /// <inheritdoc />
    public partial class profilepicstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ProfilePicId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Users_UserId",
                table: "Attachments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Users_UserId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePicId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePicId",
                table: "Users",
                column: "ProfilePicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ProfilePicId",
                table: "Users",
                column: "ProfilePicId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }
    }
}
