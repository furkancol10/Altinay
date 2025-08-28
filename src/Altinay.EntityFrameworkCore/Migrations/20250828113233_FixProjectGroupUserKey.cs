using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectGroupUserKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectGroupUser_AppProjectGroup_Id",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProjectGroupUser",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.DropIndex(
                name: "IX_AppProjectGroupUser_ProjectGroupId",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProjectGroupUser",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                columns: new[] { "ProjectGroupId", "IdentityUserId" });

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
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppProjectGroupUser",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.DropIndex(
                name: "IX_AppProjectGroupUser_ProjectGroupId_IdentityUserId",
                schema: "Altinay",
                table: "AppProjectGroupUser");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppProjectGroupUser",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectGroupUser_ProjectGroupId",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                column: "ProjectGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectGroupUser_AppProjectGroup_Id",
                schema: "Altinay",
                table: "AppProjectGroupUser",
                column: "Id",
                principalSchema: "Altinay",
                principalTable: "AppProjectGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
