using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicArticleService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("082cf502-ed29-4eff-aa8c-92f2d6d1bfe5"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0f2d5495-1105-4b09-ba8d-875a73872c49"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("861c973c-d9b1-4c17-b293-3015292929d6"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("8aaf44ab-12a9-48b2-a722-6d9f4e9f76c3"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("96c152bd-5f7d-4d09-b601-603e461ad018"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("b4f3c668-c2d3-47fe-8d4d-8f6cef0f654e"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("c3908672-b7bd-4939-8518-745ff84e4da9"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("cebed78c-6a3d-498e-895d-3f50504b78c8"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e0e68a89-8cb2-4602-a10b-2be1a78a9be5"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e7e4aa51-d49d-4fdc-a7e6-c59f0841d8c4"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("eb2354be-d9d7-4ece-9d22-a0ca95c4280d"));

            migrationBuilder.AlterColumn<string>(
                name: "SubscriptionTime",
                schema: "topic-article-service",
                table: "UserTopic",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleLike",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleDislike",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleCommentLike",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleCommentDislike",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleComment",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                schema: "topic-article-service",
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicFullName", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"), null, "cybersecurity", "Cybersecurity", 0 },
                    { new Guid("d98ad359-1bd9-4a67-878b-cd45ee6c59c9"), null, "programming", "Programming", 0 },
                    { new Guid("010a8542-b355-460d-89ba-80b8c8854518"), new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"), "cybersecurity_law_regulations", "Law regulations", 0 },
                    { new Guid("18ca51dc-c1ba-4873-ae9c-bceafc11641d"), new Guid("d98ad359-1bd9-4a67-878b-cd45ee6c59c9"), "programming_projects", "Projects", 0 },
                    { new Guid("7cbf4bb9-a725-47ad-9157-ff8ed1eca61d"), new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"), "cybersecurity_laws", "Laws", 0 },
                    { new Guid("850417fa-568b-4dfb-9f6d-5bcaf31643fd"), new Guid("d98ad359-1bd9-4a67-878b-cd45ee6c59c9"), "programming_news", "News", 0 },
                    { new Guid("ccc0167e-0b76-46a7-a368-46a66da72399"), new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"), "cybersecurity_news", "News", 0 },
                    { new Guid("eb5bd072-579f-4be7-a88c-2833e113ee04"), new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"), "cybersecurity_projects", "Projects", 0 },
                    { new Guid("30e9b054-1cf0-4920-a523-e15da64ae223"), new Guid("ccc0167e-0b76-46a7-a368-46a66da72399"), "cybersecurity_news_europe", "Europe", 0 },
                    { new Guid("3360ab96-9a1f-4e14-87f5-426ea5c0fcbd"), new Guid("ccc0167e-0b76-46a7-a368-46a66da72399"), "cybersecurity_news_america", "America", 0 },
                    { new Guid("c80e8e24-f445-465e-95ab-f81a2f9ba3f2"), new Guid("ccc0167e-0b76-46a7-a368-46a66da72399"), "cybersecurity_news_asia", "Asia", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("010a8542-b355-460d-89ba-80b8c8854518"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("18ca51dc-c1ba-4873-ae9c-bceafc11641d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("30e9b054-1cf0-4920-a523-e15da64ae223"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("3360ab96-9a1f-4e14-87f5-426ea5c0fcbd"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("7cbf4bb9-a725-47ad-9157-ff8ed1eca61d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("850417fa-568b-4dfb-9f6d-5bcaf31643fd"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("c80e8e24-f445-465e-95ab-f81a2f9ba3f2"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("eb5bd072-579f-4be7-a88c-2833e113ee04"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("ccc0167e-0b76-46a7-a368-46a66da72399"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("d98ad359-1bd9-4a67-878b-cd45ee6c59c9"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("262f36ed-c72b-4ca3-a959-acefc2bcd524"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SubscriptionTime",
                schema: "topic-article-service",
                table: "UserTopic",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleLike",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleDislike",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleCommentLike",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleCommentDislike",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateTime",
                schema: "topic-article-service",
                table: "ArticleComment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
        }
    }
}
