using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class AddGanttChartFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkEndTime",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.DropColumn(
                name: "WorkStartTime",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.RenameColumn(
                name: "MotivationNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "EnableWeekendNotifications");

            migrationBuilder.RenameColumn(
                name: "MorningNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "EnableMotivationNotifications");

            migrationBuilder.RenameColumn(
                name: "EveningNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "EnableHolidayNotifications");

            migrationBuilder.RenameColumn(
                name: "BreakReminders",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "EnableDailyStartNotifications");

            migrationBuilder.AddColumn<string>(
                name: "Dependencies",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDays",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentTaskId",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledFor",
                schema: "Altinay",
                table: "AppSmartNotification",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyEndNotificationTime",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyStartNotificationTime",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EnableBreakReminders",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableDailyEndNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MotivationNotificationCount",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppUserReminder",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ReminderTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ReminderType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RelatedTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserReminder_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSmartNotification_ScheduledFor",
                schema: "Altinay",
                table: "AppSmartNotification",
                column: "ScheduledFor");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserReminder_IsActive",
                schema: "Altinay",
                table: "AppUserReminder",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserReminder_IsCompleted",
                schema: "Altinay",
                table: "AppUserReminder",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserReminder_ReminderTime",
                schema: "Altinay",
                table: "AppUserReminder",
                column: "ReminderTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserReminder_UserId",
                schema: "Altinay",
                table: "AppUserReminder",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserReminder",
                schema: "Altinay");

            migrationBuilder.DropIndex(
                name: "IX_AppSmartNotification_ScheduledFor",
                schema: "Altinay",
                table: "AppSmartNotification");

            migrationBuilder.DropColumn(
                name: "Dependencies",
                schema: "Altinay",
                table: "AppTrackingIssue");

            migrationBuilder.DropColumn(
                name: "EstimatedDays",
                schema: "Altinay",
                table: "AppTrackingIssue");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                schema: "Altinay",
                table: "AppTrackingIssue");

            migrationBuilder.DropColumn(
                name: "Progress",
                schema: "Altinay",
                table: "AppTrackingIssue");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "Altinay",
                table: "AppTrackingIssue");

            migrationBuilder.DropColumn(
                name: "ScheduledFor",
                schema: "Altinay",
                table: "AppSmartNotification");

            migrationBuilder.DropColumn(
                name: "DailyEndNotificationTime",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.DropColumn(
                name: "DailyStartNotificationTime",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.DropColumn(
                name: "EnableBreakReminders",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.DropColumn(
                name: "EnableDailyEndNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.DropColumn(
                name: "MotivationNotificationCount",
                schema: "Altinay",
                table: "AppNotificationSetting");

            migrationBuilder.RenameColumn(
                name: "EnableWeekendNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "MotivationNotifications");

            migrationBuilder.RenameColumn(
                name: "EnableMotivationNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "MorningNotifications");

            migrationBuilder.RenameColumn(
                name: "EnableHolidayNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "EveningNotifications");

            migrationBuilder.RenameColumn(
                name: "EnableDailyStartNotifications",
                schema: "Altinay",
                table: "AppNotificationSetting",
                newName: "BreakReminders");

            migrationBuilder.AddColumn<string>(
                name: "WorkEndTime",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkStartTime",
                schema: "Altinay",
                table: "AppNotificationSetting",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
