using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectTrackingExtendedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTrackingAttachment",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrackingAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTrackingAttachment_AbpUsers_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTrackingAttachment_AppTrackingIssue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppTrackingTag",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrackingTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTrackingTag_AppTrackingProject_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppTrackingTimeLog",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrackingTimeLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTrackingTimeLog_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTrackingTimeLog_AppTrackingIssue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppTrackingIssueTag",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrackingIssueTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTrackingIssueTag_AppTrackingIssue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTrackingIssueTag_AppTrackingTag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingAttachment_IssueId",
                schema: "Altinay",
                table: "AppTrackingAttachment",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingAttachment_UploadedByUserId",
                schema: "Altinay",
                table: "AppTrackingAttachment",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingIssueTag_IssueId",
                schema: "Altinay",
                table: "AppTrackingIssueTag",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingIssueTag_IssueId_TagId",
                schema: "Altinay",
                table: "AppTrackingIssueTag",
                columns: new[] { "IssueId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingIssueTag_TagId",
                schema: "Altinay",
                table: "AppTrackingIssueTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTag_Name_ProjectId",
                schema: "Altinay",
                table: "AppTrackingTag",
                columns: new[] { "Name", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTag_ProjectId",
                schema: "Altinay",
                table: "AppTrackingTag",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTimeLog_IsActive",
                schema: "Altinay",
                table: "AppTrackingTimeLog",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTimeLog_IssueId",
                schema: "Altinay",
                table: "AppTrackingTimeLog",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTimeLog_StartTime",
                schema: "Altinay",
                table: "AppTrackingTimeLog",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingTimeLog_UserId",
                schema: "Altinay",
                table: "AppTrackingTimeLog",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTrackingAttachment",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppTrackingIssueTag",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppTrackingTimeLog",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppTrackingTag",
                schema: "Altinay");
        }
    }
}
