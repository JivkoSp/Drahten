using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class DeletedThumbsUpandThumbsDownpropertiesfromArticleCommentclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbsDown",
                table: "ArticleComment");

            migrationBuilder.DropColumn(
                name: "ThumbsUp",
                table: "ArticleComment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThumbsDown",
                table: "ArticleComment",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbsUp",
                table: "ArticleComment",
                type: "integer",
                nullable: true);
        }
    }
}
