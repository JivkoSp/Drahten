using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddedtwonewpropertiesThumbsUpandThumbsDownandselfrelationshipinArticleCommentcsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentArticleCommentId",
                table: "ArticleComment",
                type: "integer",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ParentArticleCommentId",
                table: "ArticleComment",
                column: "ParentArticleCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentArticleComment_ChildArticleComments",
                table: "ArticleComment",
                column: "ParentArticleCommentId",
                principalTable: "ArticleComment",
                principalColumn: "ArticleCommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentArticleComment_ChildArticleComments",
                table: "ArticleComment");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComment_ParentArticleCommentId",
                table: "ArticleComment");

            migrationBuilder.DropColumn(
                name: "ParentArticleCommentId",
                table: "ArticleComment");

            migrationBuilder.DropColumn(
                name: "ThumbsDown",
                table: "ArticleComment");

            migrationBuilder.DropColumn(
                name: "ThumbsUp",
                table: "ArticleComment");
        }
    }
}
