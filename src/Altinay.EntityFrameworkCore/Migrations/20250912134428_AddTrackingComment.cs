using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackingComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "Priority",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "AppTrackingComment",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssueId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTrackingComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTrackingComment_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppTrackingComment_AppTrackingIssue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "Altinay",
                        principalTable: "AppTrackingIssue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingComment_CreationTime",
                schema: "Altinay",
                table: "AppTrackingComment",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingComment_IssueId",
                schema: "Altinay",
                table: "AppTrackingComment",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrackingComment_UserId",
                schema: "Altinay",
                table: "AppTrackingComment",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTrackingComment",
                schema: "Altinay");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                schema: "Altinay",
                table: "AppTrackingIssue",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
