using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public-history-service");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "public-history-service",
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
                schema: "public-history-service",
                columns: table => new
                {
                    CommentedArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleComment = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentedArticle", x => x.CommentedArticleId);
                    table.ForeignKey(
                        name: "FK_User_CommentedArticles",
                        column: x => x.UserId,
                        principalSchema: "public-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedArticleData",
                schema: "public-history-service",
                columns: table => new
                {
                    SearchedArticleDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedArticleData", x => x.SearchedArticleDataId);
                    table.ForeignKey(
                        name: "FK_User_SearchedArticles",
                        column: x => x.UserId,
                        principalSchema: "public-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedTopicData",
                schema: "public-history-service",
                columns: table => new
                {
                    SearchedTopicDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedTopicData", x => x.SearchedTopicDataId);
                    table.ForeignKey(
                        name: "FK_User_SearchedTopics",
                        column: x => x.UserId,
                        principalSchema: "public-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedArticle",
                schema: "public-history-service",
                columns: table => new
                {
                    ViewedArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedArticle", x => x.ViewedArticleId);
                    table.ForeignKey(
                        name: "FK_User_ViewedArticles",
                        column: x => x.UserId,
                        principalSchema: "public-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedUser",
                schema: "public-history-service",
                columns: table => new
                {
                    ViewedUserReadModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewerUserId = table.Column<string>(type: "text", nullable: false),
                    ViewedUserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedUser", x => x.ViewedUserReadModelId);
                    table.ForeignKey(
                        name: "FK_User_ViewedUsers",
                        column: x => x.ViewerUserId,
                        principalSchema: "public-history-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentedArticle_UserId",
                schema: "public-history-service",
                table: "CommentedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleData_UserId",
                schema: "public-history-service",
                table: "SearchedArticleData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicData_UserId",
                schema: "public-history-service",
                table: "SearchedTopicData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedArticle_UserId",
                schema: "public-history-service",
                table: "ViewedArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUser_ViewerUserId",
                schema: "public-history-service",
                table: "ViewedUser",
                column: "ViewerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentedArticle",
                schema: "public-history-service");

            migrationBuilder.DropTable(
                name: "SearchedArticleData",
                schema: "public-history-service");

            migrationBuilder.DropTable(
                name: "SearchedTopicData",
                schema: "public-history-service");

            migrationBuilder.DropTable(
                name: "ViewedArticle",
                schema: "public-history-service");

            migrationBuilder.DropTable(
                name: "ViewedUser",
                schema: "public-history-service");

            migrationBuilder.DropTable(
                name: "User",
                schema: "public-history-service");
        }
    }
}
