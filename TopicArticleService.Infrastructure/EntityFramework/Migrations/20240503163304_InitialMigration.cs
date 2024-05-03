using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicArticleService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "topic-article-service");

            migrationBuilder.CreateTable(
                name: "Topic",
                schema: "topic-article-service",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    TopicName = table.Column<string>(type: "text", nullable: false),
                    TopicFullName = table.Column<string>(type: "text", nullable: false),
                    ParentTopicId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_ParentTopic_ChildTopics",
                        column: x => x.ParentTopicId,
                        principalSchema: "topic-article-service",
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "topic-article-service",
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
                name: "Article",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    PrevTitle = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PublishingDate = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Topic_Articles",
                        column: x => x.TopicId,
                        principalSchema: "topic-article-service",
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserTopic",
                schema: "topic-article-service",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopic", x => new { x.UserId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_Topic_Users",
                        column: x => x.TopicId,
                        principalSchema: "topic-article-service",
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Topics",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComment",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ParentArticleCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArticleId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComment", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_Article_ArticleComments",
                        column: x => x.ArticleId,
                        principalSchema: "topic-article-service",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentArticleComment_ChildArticleComments",
                        column: x => x.ParentArticleCommentId,
                        principalSchema: "topic-article-service",
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleComments",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleDislike",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleDislike", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Article_ArticleDislikes",
                        column: x => x.ArticleId,
                        principalSchema: "topic-article-service",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleDislikes",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ArticleLike",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLike", x => new { x.ArticleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Article_ArticleLikes",
                        column: x => x.ArticleId,
                        principalSchema: "topic-article-service",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleLikes",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserArticle",
                schema: "topic-article-service",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticle", x => new { x.UserId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_Article_UserArticles",
                        column: x => x.ArticleId,
                        principalSchema: "topic-article-service",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UserArticles",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCommentDislike",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentDislike", x => new { x.ArticleCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleCommentDislikes",
                        column: x => x.ArticleCommentId,
                        principalSchema: "topic-article-service",
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleCommentDislikes",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCommentLike",
                schema: "topic-article-service",
                columns: table => new
                {
                    ArticleCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentLike", x => new { x.ArticleCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleCommentLikes",
                        column: x => x.ArticleCommentId,
                        principalSchema: "topic-article-service",
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleCommentLikes",
                        column: x => x.UserId,
                        principalSchema: "topic-article-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                schema: "topic-article-service",
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicFullName", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("e7e4aa51-d49d-4fdc-a7e6-c59f0841d8c4"), null, "programming", "Programming", 0 },
                    { new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"), null, "cybersecurity", "Cybersecurity", 0 },
                    { new Guid("082cf502-ed29-4eff-aa8c-92f2d6d1bfe5"), new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"), "cybersecurity_projects", "Projects", 0 },
                    { new Guid("861c973c-d9b1-4c17-b293-3015292929d6"), new Guid("e7e4aa51-d49d-4fdc-a7e6-c59f0841d8c4"), "programming_projects", "Projects", 0 },
                    { new Guid("b4f3c668-c2d3-47fe-8d4d-8f6cef0f654e"), new Guid("e7e4aa51-d49d-4fdc-a7e6-c59f0841d8c4"), "programming_news", "News", 0 },
                    { new Guid("c3908672-b7bd-4939-8518-745ff84e4da9"), new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"), "cybersecurity_law_regulations", "Law regulations", 0 },
                    { new Guid("cebed78c-6a3d-498e-895d-3f50504b78c8"), new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"), "cybersecurity_laws", "Laws", 0 },
                    { new Guid("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"), new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"), "cybersecurity_news", "News", 0 },
                    { new Guid("0f2d5495-1105-4b09-ba8d-875a73872c49"), new Guid("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"), "cybersecurity_news_asia", "Asia", 0 },
                    { new Guid("8aaf44ab-12a9-48b2-a722-6d9f4e9f76c3"), new Guid("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"), "cybersecurity_news_europe", "Europe", 0 },
                    { new Guid("96c152bd-5f7d-4d09-b601-603e461ad018"), new Guid("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"), "cybersecurity_news_america", "America", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_TopicId",
                schema: "topic-article-service",
                table: "Article",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ArticleId",
                schema: "topic-article-service",
                table: "ArticleComment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ParentArticleCommentId",
                schema: "topic-article-service",
                table: "ArticleComment",
                column: "ParentArticleCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_UserId",
                schema: "topic-article-service",
                table: "ArticleComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentDislike_UserId",
                schema: "topic-article-service",
                table: "ArticleCommentDislike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCommentLike_UserId",
                schema: "topic-article-service",
                table: "ArticleCommentLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleDislike_UserId",
                schema: "topic-article-service",
                table: "ArticleDislike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLike_UserId",
                schema: "topic-article-service",
                table: "ArticleLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ParentTopicId",
                schema: "topic-article-service",
                table: "Topic",
                column: "ParentTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticle_ArticleId",
                schema: "topic-article-service",
                table: "UserArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopic_TopicId",
                schema: "topic-article-service",
                table: "UserTopic",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCommentDislike",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "ArticleCommentLike",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "ArticleDislike",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "ArticleLike",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "UserArticle",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "UserTopic",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "ArticleComment",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "Article",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "User",
                schema: "topic-article-service");

            migrationBuilder.DropTable(
                name: "Topic",
                schema: "topic-article-service");
        }
    }
}
