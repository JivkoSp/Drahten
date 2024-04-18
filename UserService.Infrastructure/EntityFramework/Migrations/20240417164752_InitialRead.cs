using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user-service");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "user-service",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    UserFullName = table.Column<string>(type: "text", nullable: false),
                    UserNickName = table.Column<string>(type: "text", nullable: false),
                    UserEmailAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BannedUser",
                schema: "user-service",
                columns: table => new
                {
                    IssuerUserId = table.Column<string>(type: "text", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedUser", x => new { x.IssuerUserId, x.ReceiverUserId });
                    table.ForeignKey(
                        name: "FK_Issuer_BannedUsers",
                        column: x => x.IssuerUserId,
                        principalSchema: "user-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receiver_BannedUsers",
                        column: x => x.ReceiverUserId,
                        principalSchema: "user-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequest",
                schema: "user-service",
                columns: table => new
                {
                    IssuerUserId = table.Column<string>(type: "text", nullable: false),
                    ReceiverUserId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequest", x => new { x.IssuerUserId, x.ReceiverUserId });
                    table.ForeignKey(
                        name: "FK_Issuer_ContactRequests",
                        column: x => x.IssuerUserId,
                        principalSchema: "user-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receiver_ContactRequests",
                        column: x => x.ReceiverUserId,
                        principalSchema: "user-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTracking",
                schema: "user-service",
                columns: table => new
                {
                    UserTrackingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Referrer = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTracking", x => x.UserTrackingId);
                    table.ForeignKey(
                        name: "FK_User_AuditTrail",
                        column: x => x.UserId,
                        principalSchema: "user-service",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannedUser_ReceiverUserId",
                schema: "user-service",
                table: "BannedUser",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_ReceiverUserId",
                schema: "user-service",
                table: "ContactRequest",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTracking_UserId",
                schema: "user-service",
                table: "UserTracking",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedUser",
                schema: "user-service");

            migrationBuilder.DropTable(
                name: "ContactRequest",
                schema: "user-service");

            migrationBuilder.DropTable(
                name: "UserTracking",
                schema: "user-service");

            migrationBuilder.DropTable(
                name: "User",
                schema: "user-service");
        }
    }
}
