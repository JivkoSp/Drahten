using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicArticleService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedTopicFullNamepropertyintheTopicmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("874a90d4-97a5-4b80-9d4c-af88d34d71e4"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("8e329cd3-1ffa-4464-a454-71b44df027d2"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("9573a78e-471f-436e-8c8b-2b6988cc1f76"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("95a886ff-e51e-454e-be48-28a15c98d121"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("9be4c36b-9214-46fc-8ea8-29d6226ec208"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("9e38ecc1-5da4-499e-9104-158411b9bf29"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("ae490ff7-046b-410b-8bb2-4cc2ca9dd4e5"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("ef296ddf-fe84-42b9-93c6-e51e4a138915"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("98200215-09b8-4c1f-9d7d-b101da584fa2"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("fe1071a7-4544-48e6-8e7f-38721187991d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"));

            migrationBuilder.InsertData(
                schema: "topic-article-service",
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicFullName", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("17c50c00-2c7e-44c6-a490-e576eb9da3f6"), null, "programming", "Programming", 0 },
                    { new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"), null, "cybersecurity", "Cybersecurity", 0 },
                    { new Guid("2db7c75c-3eea-4c91-ad84-69c5b0fad176"), new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"), "cybersecurity_projects", "Projects", 0 },
                    { new Guid("708707f4-50b8-4f43-8da5-3f07d6644e59"), new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"), "cybersecurity_laws", "Laws", 0 },
                    { new Guid("8032b797-723d-4019-abc9-b5b8aa7c0cb9"), new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"), "cybersecurity_law_regulations", "Law regulations", 0 },
                    { new Guid("9194c4f2-7789-4995-9f92-ca5bb997cf73"), new Guid("17c50c00-2c7e-44c6-a490-e576eb9da3f6"), "programming_news", "News", 0 },
                    { new Guid("e1712972-5d22-4bc5-be12-4bdb9de44386"), new Guid("17c50c00-2c7e-44c6-a490-e576eb9da3f6"), "programming_projects", "Projects", 0 },
                    { new Guid("f25a86ca-37d9-41c0-b01a-03303dfed27d"), new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"), "cybersecurity_news", "News", 0 },
                    { new Guid("0993403f-51c3-4d05-863c-e745845f8050"), new Guid("f25a86ca-37d9-41c0-b01a-03303dfed27d"), "cybersecurity_news_america", "America", 0 },
                    { new Guid("480fb040-2768-47d2-a065-30a7deaedf6a"), new Guid("f25a86ca-37d9-41c0-b01a-03303dfed27d"), "cybersecurity_news_asia", "Asia", 0 },
                    { new Guid("e983ed0d-20e2-43fb-b5bd-559bc6262906"), new Guid("f25a86ca-37d9-41c0-b01a-03303dfed27d"), "cybersecurity_news_europe", "Europe", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0993403f-51c3-4d05-863c-e745845f8050"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("2db7c75c-3eea-4c91-ad84-69c5b0fad176"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("480fb040-2768-47d2-a065-30a7deaedf6a"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("708707f4-50b8-4f43-8da5-3f07d6644e59"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("8032b797-723d-4019-abc9-b5b8aa7c0cb9"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("9194c4f2-7789-4995-9f92-ca5bb997cf73"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e1712972-5d22-4bc5-be12-4bdb9de44386"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e983ed0d-20e2-43fb-b5bd-559bc6262906"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("17c50c00-2c7e-44c6-a490-e576eb9da3f6"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("f25a86ca-37d9-41c0-b01a-03303dfed27d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("7a954d4e-2483-433a-ac1a-dc1c5108daed"));

            migrationBuilder.InsertData(
                schema: "topic-article-service",
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicFullName", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"), null, "cybersecurity", "Cybersecurity", 0 },
                    { new Guid("fe1071a7-4544-48e6-8e7f-38721187991d"), null, "programming", "Programming", 0 },
                    { new Guid("8e329cd3-1ffa-4464-a454-71b44df027d2"), new Guid("fe1071a7-4544-48e6-8e7f-38721187991d"), "programming_projects", "Projects", 0 },
                    { new Guid("9573a78e-471f-436e-8c8b-2b6988cc1f76"), new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"), "cybersecurity_law_regulations", "Law regulations", 0 },
                    { new Guid("95a886ff-e51e-454e-be48-28a15c98d121"), new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"), "cybersecurity_laws", "Laws", 0 },
                    { new Guid("98200215-09b8-4c1f-9d7d-b101da584fa2"), new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"), "cybersecurity_news", "News", 0 },
                    { new Guid("9be4c36b-9214-46fc-8ea8-29d6226ec208"), new Guid("fe1071a7-4544-48e6-8e7f-38721187991d"), "programming_news", "News", 0 },
                    { new Guid("9e38ecc1-5da4-499e-9104-158411b9bf29"), new Guid("f4d95a41-f70a-4aab-aee1-688fbe7e6985"), "cybersecurity_projects", "Projects", 0 },
                    { new Guid("874a90d4-97a5-4b80-9d4c-af88d34d71e4"), new Guid("98200215-09b8-4c1f-9d7d-b101da584fa2"), "cybersecurity_news_europe", "Europe", 0 },
                    { new Guid("ae490ff7-046b-410b-8bb2-4cc2ca9dd4e5"), new Guid("98200215-09b8-4c1f-9d7d-b101da584fa2"), "cybersecurity_news_america", "America", 0 },
                    { new Guid("ef296ddf-fe84-42b9-93c6-e51e4a138915"), new Guid("98200215-09b8-4c1f-9d7d-b101da584fa2"), "cybersecurity_news_asia", "Asia", 0 }
                });
        }
    }
}
