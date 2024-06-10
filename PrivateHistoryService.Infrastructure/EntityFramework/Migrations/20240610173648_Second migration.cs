using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ViewedUsers",
                schema: "private-history-service",
                table: "ViewedUser");

            migrationBuilder.DropIndex(
                name: "IX_ViewedUser_ViewedUserId",
                schema: "private-history-service",
                table: "ViewedUser");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "CommentedArticle",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUser_ViewerUserId",
                schema: "private-history-service",
                table: "ViewedUser",
                column: "ViewerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ViewedUsers",
                schema: "private-history-service",
                table: "ViewedUser",
                column: "ViewerUserId",
                principalSchema: "private-history-service",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ViewedUsers",
                schema: "private-history-service",
                table: "ViewedUser");

            migrationBuilder.DropIndex(
                name: "IX_ViewedUser_ViewerUserId",
                schema: "private-history-service",
                table: "ViewedUser");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "CommentedArticle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUser_ViewedUserId",
                schema: "private-history-service",
                table: "ViewedUser",
                column: "ViewedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ViewedUsers",
                schema: "private-history-service",
                table: "ViewedUser",
                column: "ViewedUserId",
                principalSchema: "private-history-service",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
