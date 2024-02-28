using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddednewpropertiesinArticleclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleData",
                table: "Article",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrevTitle",
                table: "Article",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "PrevTitle",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Article",
                newName: "ArticleData");
        }
    }
}
