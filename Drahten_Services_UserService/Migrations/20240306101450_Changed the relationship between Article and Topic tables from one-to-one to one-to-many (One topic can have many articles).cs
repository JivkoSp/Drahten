using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangedtherelationshipbetweenArticleandTopictablesfromonetoonetoonetomanyOnetopiccanhavemanyarticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_TopicId",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TopicId",
                table: "Article",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_TopicId",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TopicId",
                table: "Article",
                column: "TopicId",
                unique: true);
        }
    }
}
