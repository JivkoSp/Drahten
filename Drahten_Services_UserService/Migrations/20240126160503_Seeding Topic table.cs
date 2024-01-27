using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTopictable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: 1);
        }
    }
}
