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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    { new Guid("4e75ec3e-b827-4322-a07c-9d555befe301"), null, "Programming", 0 },
                    { new Guid("c7b236dc-ebfd-4de2-ab5d-b15a25a77b2c"), null, "Cybersecurity", 0 },
                    { new Guid("1a926c80-aac1-4b44-8992-d8e6359736d6"), new Guid("c7b236dc-ebfd-4de2-ab5d-b15a25a77b2c"), "Laws", 0 },
                    { new Guid("4a21e2a8-a20a-4177-a98e-44c1d3e8dd5f"), new Guid("4e75ec3e-b827-4322-a07c-9d555befe301"), "News", 0 },
                    { new Guid("b2a03f3d-6d0a-4ca4-8220-94cc2d09e10a"), new Guid("c7b236dc-ebfd-4de2-ab5d-b15a25a77b2c"), "Law regulations", 0 },
                    { new Guid("be782bb9-3591-4206-8052-76e5112576b7"), new Guid("c7b236dc-ebfd-4de2-ab5d-b15a25a77b2c"), "News", 0 },
                    { new Guid("ce9a643a-d3e0-4e7f-a3f6-dc0f193f2ae0"), new Guid("4e75ec3e-b827-4322-a07c-9d555befe301"), "Projects", 0 },
                    { new Guid("e22e67b5-af49-4e4e-b488-7c41681ac7d8"), new Guid("c7b236dc-ebfd-4de2-ab5d-b15a25a77b2c"), "Projects", 0 },
                    { new Guid("9587a7ea-ee3c-49d5-b809-824211f4c2df"), new Guid("be782bb9-3591-4206-8052-76e5112576b7"), "Asia", 0 },
                    { new Guid("d76aa768-d06d-4485-bcdb-498105b1ee92"), new Guid("be782bb9-3591-4206-8052-76e5112576b7"), "America", 0 },
                    { new Guid("fcf49b1c-e8d1-4134-a4cd-9ee01fac7383"), new Guid("be782bb9-3591-4206-8052-76e5112576b7"), "Europe", 0 }
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
