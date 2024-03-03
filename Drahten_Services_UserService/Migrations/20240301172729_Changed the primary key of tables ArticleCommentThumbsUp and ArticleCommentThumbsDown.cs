using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangedtheprimarykeyoftablesArticleCommentThumbsUpandArticleCommentThumbsDown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCommentThumbsUp",
                table: "ArticleCommentThumbsUp");

            migrationBuilder.DropIndex(
                name: "IX_ArticleCommentThumbsUp_UserId",
                table: "ArticleCommentThumbsUp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCommentThumbsDown",
                table: "ArticleCommentThumbsDown");

            migrationBuilder.DropIndex(
                name: "IX_ArticleCommentThumbsDown_UserId",
                table: "ArticleCommentThumbsDown");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCommentThumbsUp",
                table: "ArticleCommentThumbsUp",
                columns: new[] { "ArticleCommentId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCommentThumbsDown",
                table: "ArticleCommentThumbsDown",
                columns: new[] { "ArticleCommentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsUp_UserId",
                table: "ArticleCommentThumbsUp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsDown_UserId",
                table: "ArticleCommentThumbsDown",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCommentThumbsUp",
                table: "ArticleCommentThumbsUp");

            migrationBuilder.DropIndex(
                name: "IX_ArticleCommentThumbsUp_UserId",
                table: "ArticleCommentThumbsUp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCommentThumbsDown",
                table: "ArticleCommentThumbsDown");

            migrationBuilder.DropIndex(
                name: "IX_ArticleCommentThumbsDown_UserId",
                table: "ArticleCommentThumbsDown");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCommentThumbsUp",
                table: "ArticleCommentThumbsUp",
                column: "ArticleCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCommentThumbsDown",
                table: "ArticleCommentThumbsDown",
                column: "ArticleCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsUp_UserId",
                table: "ArticleCommentThumbsUp",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsDown_UserId",
                table: "ArticleCommentThumbsDown",
                column: "UserId",
                unique: true);
        }
    }
}
