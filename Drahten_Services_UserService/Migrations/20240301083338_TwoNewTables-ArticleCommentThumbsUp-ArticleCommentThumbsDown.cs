using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class TwoNewTablesArticleCommentThumbsUpArticleCommentThumbsDown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCommentThumbsDown",
                columns: table => new
                {
                    ArticleCommentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentThumbsDown", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleCommentThumbsDown",
                        column: x => x.ArticleCommentId,
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleCommentThumbsDown",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCommentThumbsUp",
                columns: table => new
                {
                    ArticleCommentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentThumbsUp", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleCommentThumbsUp",
                        column: x => x.ArticleCommentId,
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleCommentThumbsUp",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsDown_UserId",
                table: "ArticleCommentThumbsDown",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentThumbsUp_UserId",
                table: "ArticleCommentThumbsUp",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCommentThumbsDown");

            migrationBuilder.DropTable(
                name: "ArticleCommentThumbsUp");
        }
    }
}
