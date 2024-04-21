using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopicArticleService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedVersionpropertyinUserReadModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("2670e81a-8bd7-423a-acd4-5892660934d8"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("4204c27a-aed5-459e-a245-58978895875f"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("45f672df-8bdb-456c-8ea1-dd5b0b867df7"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("76681b14-e42a-47c7-b19f-954cba4c5dd9"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("80dfc5a1-7813-456d-a7b2-4b8e93e81f16"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("888a5c96-7c7c-4f98-90b6-91a0c2d401b0"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("c9523aef-2faa-4e89-8599-43b3089b5960"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("edd8a756-6238-45cf-9275-4037af21b388"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("51f85e58-0de3-4b03-9876-d3ce0c3abcf6"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("5ae9a3dc-d76c-4b4d-8a1f-a17fe5c817eb"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("44aaeb3f-cae7-4a8c-93a6-2b13deddd3bd"));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "topic-article-service",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                schema: "topic-article-service",
                table: "Topic",
                columns: new[] { "TopicId", "ParentTopicId", "TopicName", "Version" },
                values: new object[,]
                {
                    { new Guid("0e48cadd-992b-4007-9a77-d86b31519496"), null, "Cybersecurity", 0 },
                    { new Guid("f8253ad7-b003-475e-851a-91f0100d7948"), null, "Programming", 0 },
                    { new Guid("0f349f0b-5291-4acc-8979-83de61a2a53d"), new Guid("f8253ad7-b003-475e-851a-91f0100d7948"), "Projects", 0 },
                    { new Guid("300ecb1c-a85d-466c-8d9c-eb70180a3f7b"), new Guid("0e48cadd-992b-4007-9a77-d86b31519496"), "News", 0 },
                    { new Guid("36822d96-b610-4b9a-a81a-f6d9cd5f5eb2"), new Guid("0e48cadd-992b-4007-9a77-d86b31519496"), "Laws", 0 },
                    { new Guid("cf27c09d-b91d-4b94-9293-e4f8a9cecfad"), new Guid("0e48cadd-992b-4007-9a77-d86b31519496"), "Projects", 0 },
                    { new Guid("e056cfad-77a6-4be3-adff-5b848c0ee0f6"), new Guid("0e48cadd-992b-4007-9a77-d86b31519496"), "Law regulations", 0 },
                    { new Guid("f8f4ccd5-6395-4f30-9168-0678c63ea457"), new Guid("f8253ad7-b003-475e-851a-91f0100d7948"), "News", 0 },
                    { new Guid("a3c436bc-9dda-4e33-80fe-0a888a18f5bb"), new Guid("300ecb1c-a85d-466c-8d9c-eb70180a3f7b"), "Europe", 0 },
                    { new Guid("be7b0088-c4b8-4ef1-846a-628f6f6d267d"), new Guid("300ecb1c-a85d-466c-8d9c-eb70180a3f7b"), "Asia", 0 },
                    { new Guid("fa56494a-83f8-47cd-98eb-739b8f1d7409"), new Guid("300ecb1c-a85d-466c-8d9c-eb70180a3f7b"), "America", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0f349f0b-5291-4acc-8979-83de61a2a53d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("36822d96-b610-4b9a-a81a-f6d9cd5f5eb2"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("a3c436bc-9dda-4e33-80fe-0a888a18f5bb"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("be7b0088-c4b8-4ef1-846a-628f6f6d267d"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("cf27c09d-b91d-4b94-9293-e4f8a9cecfad"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e056cfad-77a6-4be3-adff-5b848c0ee0f6"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("f8f4ccd5-6395-4f30-9168-0678c63ea457"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("fa56494a-83f8-47cd-98eb-739b8f1d7409"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("300ecb1c-a85d-466c-8d9c-eb70180a3f7b"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("f8253ad7-b003-475e-851a-91f0100d7948"));

            migrationBuilder.DeleteData(
                schema: "topic-article-service",
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0e48cadd-992b-4007-9a77-d86b31519496"));

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "topic-article-service",
                table: "User");

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
        }
    }
}
