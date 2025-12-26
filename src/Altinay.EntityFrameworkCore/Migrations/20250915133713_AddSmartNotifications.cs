using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class AddSmartNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppNotificationSetting",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MorningNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    EveningNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    BreakReminders = table.Column<bool>(type: "boolean", nullable: false),
                    MotivationNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    BreakReminderInterval = table.Column<int>(type: "integer", nullable: false),
                    WorkStartTime = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    WorkEndTime = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    SoundEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    VibrationEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNotificationSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppNotificationSetting_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSmartNotification",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReadAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSmartNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSmartNotification_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppNotificationSetting_UserId",
                schema: "Altinay",
                table: "AppNotificationSetting",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_IsRead",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_IsSent",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "IsSent");

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_NotificationType",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "NotificationType");

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_SentAt",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "SentAt");

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_UserId",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppNotificationSetting",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppSmartNotification",
                schema: "Altinay");
        }
    }
}
