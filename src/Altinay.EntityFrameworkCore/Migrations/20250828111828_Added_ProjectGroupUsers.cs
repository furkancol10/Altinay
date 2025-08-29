using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Added_ProjectGroupUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectGroupUser",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectGroupUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectGroupUser_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectGroupUser_AppProjectGroup_Id",
                        column: x => x.Id,
                        principalSchema: "Altinay",
                        principalTable: "AppProjectGroup",
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
                name: "IX_AppProjectGroupUser_ProjectGroupId",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                column: "ProjectGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectGroupUser",
                schema: "Altinay");
        }
    }
}
