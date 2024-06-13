using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedRetentionUntilpropertyoftypeDateTimeOffsettoUserReadModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RetentionUntil",
                schema: "private-history-service",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetentionUntil",
                schema: "private-history-service",
                table: "User");
        }
    }
}
