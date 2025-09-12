using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Created_ProjectGroups_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectGroup",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileAliasId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectGroupUser",
                schema: "Altinay",
                columns: table => new
                {
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectGroupUser", x => new { x.ProjectGroupId, x.IdentityUserId });
                    table.ForeignKey(
                        name: "FK_AppProjectGroupUser_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectGroupUser_AppProjectGroup_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalSchema: "Altinay",
                        principalTable: "AppProjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectGroupUser_IdentityUserId",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectGroupUser_ProjectGroupId_IdentityUserId",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                columns: new[] { "ProjectGroupId", "IdentityUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectGroupUser",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppProjectGroup",
                schema: "Altinay");
        }
    }
}
