using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ChangeallDateTimecolumntypestostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "ViewedUser",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "ViewedArticle",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "TopicSubscription",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "SearchedTopicData",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "SearchedArticleData",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "LikedArticleComment",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "LikedArticle",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "DislikedArticleComment",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "private-history-service",
                table: "DislikedArticle",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "ViewedUser",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "ViewedArticle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "TopicSubscription",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "SearchedTopicData",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "SearchedArticleData",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "LikedArticleComment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "LikedArticle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "DislikedArticleComment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "private-history-service",
                table: "DislikedArticle",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
