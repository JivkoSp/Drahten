using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Self_OnetoMany_Relationship_To_TopicModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentTopicId",
                table: "Topic",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ParentTopicId",
                table: "Topic",
                column: "ParentTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentTopic_ChildTopics",
                table: "Topic",
                column: "ParentTopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentTopic_ChildTopics",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Topic_ParentTopicId",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "ParentTopicId",
                table: "Topic");
        }
    }
}
