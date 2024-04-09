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
                columns: new[] { "TopicId", "ParentTopicId", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"), null, "Cybersecurity", 0 },
                    { new Guid("51f85e58-0de3-4b03-9876-d3ce0c3abcf6"), null, "Programming", 0 },
                    { new Guid("2670e81a-8bd7-423a-acd4-5892660934d8"), new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"), "Projects", 0 },
                    { new Guid("4204c27a-aed5-459e-a245-58978895875f"), new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"), "Law regulations", 0 },
                    { new Guid("45f672df-8bdb-456c-8ea1-dd5b0b867df7"), new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"), "Laws", 0 },
                    { new Guid("5ae9a3dc-d76c-4b4d-8a1f-a17fe5c817eb"), new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"), "News", 0 },
                    { new Guid("c9523aef-2faa-4e89-8599-43b3089b5960"), new Guid("51f85e58-0de3-4b03-9876-d3ce0c3abcf6"), "Projects", 0 },
                    { new Guid("edd8a756-6238-45cf-9275-4037af21b388"), new Guid("51f85e58-0de3-4b03-9876-d3ce0c3abcf6"), "News", 0 },
                    { new Guid("76681b14-e42a-47c7-b19f-954cba4c5dd9"), new Guid("5ae9a3dc-d76c-4b4d-8a1f-a17fe5c817eb"), "America", 0 },
                    { new Guid("80dfc5a1-7813-456d-a7b2-4b8e93e81f16"), new Guid("5ae9a3dc-d76c-4b4d-8a1f-a17fe5c817eb"), "Asia", 0 },
                    { new Guid("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"), new Guid("5ae9a3dc-d76c-4b4d-8a1f-a17fe5c817eb"), "Europe", 0 }
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
