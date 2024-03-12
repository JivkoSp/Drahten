using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddedtwonewtablesSearchedArticleDataPrivateHistandSearchedArticleDataPublicHist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchedArticleDataPrivateHist",
                columns: table => new
                {
                    SearchedArticleDataPrivateHistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    SearchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    PrivateHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedArticleDataPrivateHist", x => x.SearchedArticleDataPrivateHistId);
                    table.ForeignKey(
                        name: "FK_Article_SearchedArticleDataPrivateHist",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PrivateHistory_SearchedArticleDataPrivateHist",
                        column: x => x.PrivateHistoryId,
                        principalTable: "PrivateHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchedArticleDataPublicHist",
                columns: table => new
                {
                    SearchedArticleDataPublicHistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SearchedData = table.Column<string>(type: "text", nullable: false),
                    SearchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArticleId = table.Column<string>(type: "text", nullable: false),
                    PublicHistoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedArticleDataPublicHist", x => x.SearchedArticleDataPublicHistId);
                    table.ForeignKey(
                        name: "FK_Article_SearchedArticleDataPublicHist",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PublicHistory_SearchedArticleDataPublicHist",
                        column: x => x.PublicHistoryId,
                        principalTable: "PublicHistory",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleDataPrivateHist_ArticleId",
                table: "SearchedArticleDataPrivateHist",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleDataPrivateHist_PrivateHistoryId",
                table: "SearchedArticleDataPrivateHist",
                column: "PrivateHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleDataPublicHist_ArticleId",
                table: "SearchedArticleDataPublicHist",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedArticleDataPublicHist_PublicHistoryId",
                table: "SearchedArticleDataPublicHist",
                column: "PublicHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchedArticleDataPrivateHist");

            migrationBuilder.DropTable(
                name: "SearchedArticleDataPublicHist");
        }
    }
}
