using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedSearchedDataAnswerpropertyoftypestringinSearchedArticleDataReadModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchedDataAnswer",
                schema: "private-history-service",
                table: "SearchedArticleData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchedDataAnswer",
                schema: "private-history-service",
                table: "SearchedArticleData");
        }
    }
}
