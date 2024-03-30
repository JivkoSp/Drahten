using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicArticleService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialRead : Migration
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
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    SubscriptionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                columns: new[] { "TopicId", "ParentTopicId", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("2a9d89b5-2a69-4e06-ae6e-819795a35704"), null, "Programming", 0 },
                    { new Guid("eb5756b6-e0b9-4a08-9067-92e9af115ed7"), null, "Cybersecurity", 0 },
                    { new Guid("33f17cb1-833d-409f-99b0-ae298ff6d1dc"), new Guid("2a9d89b5-2a69-4e06-ae6e-819795a35704"), "News", 0 },
                    { new Guid("35d2341d-2e8e-4732-b1fe-ff45192bb327"), new Guid("eb5756b6-e0b9-4a08-9067-92e9af115ed7"), "Projects", 0 },
                    { new Guid("37ca1cc4-deab-4ee2-b81f-503829a52cf2"), new Guid("eb5756b6-e0b9-4a08-9067-92e9af115ed7"), "Laws", 0 },
                    { new Guid("b0dbdc91-d64b-4f01-86f7-f205b903b6ef"), new Guid("2a9d89b5-2a69-4e06-ae6e-819795a35704"), "Projects", 0 },
                    { new Guid("bc97ad09-44ac-4a74-a6c5-6b0ccf0cab63"), new Guid("eb5756b6-e0b9-4a08-9067-92e9af115ed7"), "News", 0 },
                    { new Guid("e01e9d76-2fed-402a-b344-e349b2ed679e"), new Guid("eb5756b6-e0b9-4a08-9067-92e9af115ed7"), "Law regulations", 0 },
                    { new Guid("444de8d2-4334-4b91-8f56-8c01815d21df"), new Guid("bc97ad09-44ac-4a74-a6c5-6b0ccf0cab63"), "Europe", 0 },
                    { new Guid("8eebb631-9b69-4fe7-925b-483ee41e007c"), new Guid("bc97ad09-44ac-4a74-a6c5-6b0ccf0cab63"), "America", 0 },
                    { new Guid("db1e3c53-a7e1-40a2-90cf-4cb3e6dc0718"), new Guid("bc97ad09-44ac-4a74-a6c5-6b0ccf0cab63"), "Asia", 0 }
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
