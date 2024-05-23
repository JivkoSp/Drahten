using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "private-history-service");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "private-history-service",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CommentedArticle",
                schema: "private-history-service",
                columns: table => new
                {
                    CommentedArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleComment = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentedArticle", x => x.CommentedArticleId);
                    table.ForeignKey(
                        name: "FK_User_CommentedArticles",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DislikedArticle",
                schema: "private-history-service",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikedArticle", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_DislikedArticles",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DislikedArticleComment",
                schema: "private-history-service",
                columns: table => new
                {
                    ArticleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikedArticleComment", x => new { x.ArticleCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_DislikedArticleComments",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedArticle",
                schema: "private-history-service",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedArticle", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_LikedArticles",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedArticleComment",
                schema: "private-history-service",
                columns: table => new
                {
                    ArticleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedArticleComment", x => new { x.ArticleCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_LikedArticleComments",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedArticleData",
                schema: "private-history-service",
                columns: table => new
                {
                    SearchedArticleDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedArticleData", x => x.SearchedArticleDataId);
                    table.ForeignKey(
                        name: "FK_User_SearchedArticles",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedTopicData",
                schema: "private-history-service",
                columns: table => new
                {
                    SearchedTopicDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedTopicData", x => x.SearchedTopicDataId);
                    table.ForeignKey(
                        name: "FK_User_SearchedTopics",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicSubscription",
                schema: "private-history-service",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicSubscription", x => new { x.TopicId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_TopicSubscriptions",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedArticle",
                schema: "private-history-service",
                columns: table => new
                {
                    ViewedArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedArticle", x => x.ViewedArticleId);
                    table.ForeignKey(
                        name: "FK_User_ViewedArticles",
                        column: x => x.UserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedUser",
                schema: "private-history-service",
                columns: table => new
                {
                    ViewedUserReadModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewerUserId = table.Column<string>(type: "text", nullable: false),
                    ViewedUserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedUser", x => x.ViewedUserReadModelId);
                    table.ForeignKey(
                        name: "FK_User_ViewedUsers",
                        column: x => x.ViewedUserId,
                        principalSchema: "private-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentedArticle_UserId",
                schema: "private-history-service",
                table: "CommentedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedArticle_UserId",
                schema: "private-history-service",
                table: "DislikedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedArticleComment_UserId",
                schema: "private-history-service",
                table: "DislikedArticleComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedArticle_UserId",
                schema: "private-history-service",
                table: "LikedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedArticleComment_UserId",
                schema: "private-history-service",
                table: "LikedArticleComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleData_UserId",
                schema: "private-history-service",
                table: "SearchedArticleData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicData_UserId",
                schema: "private-history-service",
                table: "SearchedTopicData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicSubscription_UserId",
                schema: "private-history-service",
                table: "TopicSubscription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedArticle_UserId",
                schema: "private-history-service",
                table: "ViewedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUser_ViewedUserId",
                schema: "private-history-service",
                table: "ViewedUser",
                column: "ViewedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentedArticle",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "DislikedArticle",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "DislikedArticleComment",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "LikedArticle",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "LikedArticleComment",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "SearchedArticleData",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "SearchedTopicData",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "TopicSubscription",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "ViewedArticle",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "ViewedUser",
                schema: "private-history-service");

            migrationBuilder.DropTable(
                name: "User",
                schema: "private-history-service");
        }
    }
}
