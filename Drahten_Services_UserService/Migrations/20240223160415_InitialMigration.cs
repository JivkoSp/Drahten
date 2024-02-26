using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TopicName = table.Column<string>(type: "text", nullable: false),
                    ParentTopicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_ParentTopic_ChildTopics",
                        column: x => x.ParentTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
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
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    ArticleData = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Topic_Article",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequest",
                columns: table => new
                {
                    ContactRequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequest", x => x.ContactRequestId);
                    table.ForeignKey(
                        name: "FK_ContactRequest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Receiver_ContactRequest",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sender_ContactRequest",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateHistory",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    HistoryLiveTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateHistory", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_PrivateHistory",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicHistory",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHistory", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_PublicHistory",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTopic",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopic", x => new { x.UserId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_Topic_Users",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Topics",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTracking",
                columns: table => new
                {
                    UserTrackingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTracking", x => x.UserTrackingId);
                    table.ForeignKey(
                        name: "FK_User_UserTracking",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComment",
                columns: table => new
                {
                    ArticleCommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComment", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_Article_ArticleComment",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ArticleComment",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleLike",
                columns: table => new
                {
                    ArticleLikeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLike", x => x.ArticleLikeId);
                    table.ForeignKey(
                        name: "FK_Article_ArticleLike",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_User_ArticleLike",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArticle",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticle", x => new { x.UserId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_Article_UserArticle",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UserArticle",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedTopicDataPrivateHist",
                columns: table => new
                {
                    SearchedTopicDataPrivateHistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    SearchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    PrivateHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedTopicDataPrivateHist", x => x.SearchedTopicDataPrivateHistId);
                    table.ForeignKey(
                        name: "FK_PrivateHistory_SearchedTopicDataPrivateHist",
                        column: x => x.PrivateHistoryId,
                        principalTable: "PrivateHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topic_SearchedTopicDataPrivateHist",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ViewedArticlePrivateHist",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    ViewTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PrivateHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedArticlePrivateHist", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Article_ViewedArticlePrivateHist",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PrivateHistory_ViewedArticlePrivateHist",
                        column: x => x.PrivateHistoryId,
                        principalTable: "PrivateHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedUserPrivateHist",
                columns: table => new
                {
                    ViewedUserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PrivateHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedUserPrivateHist", x => x.ViewedUserId);
                    table.ForeignKey(
                        name: "FK_PrivateHistory_ViewedUserPrivateHist",
                        column: x => x.PrivateHistoryId,
                        principalTable: "PrivateHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ViewedUserPrivateHist",
                        column: x => x.ViewedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SearchedTopicDataPublicHist",
                columns: table => new
                {
                    SearchedTopicDataPublicHistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    SearchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    PublicHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedTopicDataPublicHist", x => x.SearchedTopicDataPublicHistId);
                    table.ForeignKey(
                        name: "FK_PublicHistory_SearchedTopicDataPublicHist",
                        column: x => x.PublicHistoryId,
                        principalTable: "PublicHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topic_SearchedTopicDataPublicHist",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ViewedArticlePublicHist",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    ViewTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PublicHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedArticlePublicHist", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Article_ViewedArticlePublicHist",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PublicHistory_ViewedArticlePublicHist",
                        column: x => x.PublicHistoryId,
                        principalTable: "PublicHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewedUserPublicHist",
                columns: table => new
                {
                    ViewedUserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PublicHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedUserPublicHist", x => x.ViewedUserId);
                    table.ForeignKey(
                        name: "FK_PublicHistory_ViewedUserPublicHist",
                        column: x => x.PublicHistoryId,
                        principalTable: "PublicHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_ViewedUserPublicHist",
                        column: x => x.ViewedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicName" },
                values: new object[,]
                {
                    { 1, null, "Cybersecurity" },
                    { 2, null, "Programming" },
                    { 3, 1, "News" },
                    { 4, 1, "Projects" },
                    { 5, 1, "Laws" },
                    { 6, 1, "Law regulations" },
                    { 7, 2, "News" },
                    { 8, 2, "Projects" },
                    { 9, 3, "America" },
                    { 10, 3, "Asia" },
                    { 11, 3, "Europe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_TopicId",
                table: "Article",
                column: "TopicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ArticleId",
                table: "ArticleComment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_UserId",
                table: "ArticleComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLike_ArticleId",
                table: "ArticleLike",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLike_UserId",
                table: "ArticleLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_ReceiverId",
                table: "ContactRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_SenderId",
                table: "ContactRequest",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_UserId",
                table: "ContactRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicDataPrivateHist_PrivateHistoryId",
                table: "SearchedTopicDataPrivateHist",
                column: "PrivateHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicDataPrivateHist_TopicId",
                table: "SearchedTopicDataPrivateHist",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicDataPublicHist_PublicHistoryId",
                table: "SearchedTopicDataPublicHist",
                column: "PublicHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedTopicDataPublicHist_TopicId",
                table: "SearchedTopicDataPublicHist",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ParentTopicId",
                table: "Topic",
                column: "ParentTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticle_ArticleId",
                table: "UserArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopic_TopicId",
                table: "UserTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTracking_UserId",
                table: "UserTracking",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ViewedArticlePrivateHist_PrivateHistoryId",
                table: "ViewedArticlePrivateHist",
                column: "PrivateHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedArticlePublicHist_PublicHistoryId",
                table: "ViewedArticlePublicHist",
                column: "PublicHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUserPrivateHist_PrivateHistoryId",
                table: "ViewedUserPrivateHist",
                column: "PrivateHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewedUserPublicHist_PublicHistoryId",
                table: "ViewedUserPublicHist",
                column: "PublicHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleComment");

            migrationBuilder.DropTable(
                name: "ArticleLike");

            migrationBuilder.DropTable(
                name: "ContactRequest");

            migrationBuilder.DropTable(
                name: "SearchedTopicDataPrivateHist");

            migrationBuilder.DropTable(
                name: "SearchedTopicDataPublicHist");

            migrationBuilder.DropTable(
                name: "UserArticle");

            migrationBuilder.DropTable(
                name: "UserTopic");

            migrationBuilder.DropTable(
                name: "UserTracking");

            migrationBuilder.DropTable(
                name: "ViewedArticlePrivateHist");

            migrationBuilder.DropTable(
                name: "ViewedArticlePublicHist");

            migrationBuilder.DropTable(
                name: "ViewedUserPrivateHist");

            migrationBuilder.DropTable(
                name: "ViewedUserPublicHist");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "PrivateHistory");

            migrationBuilder.DropTable(
                name: "PublicHistory");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
