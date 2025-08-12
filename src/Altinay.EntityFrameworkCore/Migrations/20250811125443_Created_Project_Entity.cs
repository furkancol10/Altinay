using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Created_Project_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoom_AppBooking_FloorID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.AlterColumn<string>(
                name: "BookedBy",
                schema: "Altinay",
                table: "AppBooking",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "AppProject",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProjectDescription = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_AppProject", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoom_AppFloor_FloorID",
                schema: "Altinay",
                table: "AppRoom",
                column: "FloorID",
                principalSchema: "Altinay",
                principalTable: "AppFloor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoom_AppFloor_FloorID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropTable(
                name: "AppProject",
                schema: "Altinay");

            migrationBuilder.AlterColumn<string>(
                name: "BookedBy",
                schema: "Altinay",
                table: "AppBooking",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoom_AppBooking_FloorID",
                schema: "Altinay",
                table: "AppRoom",
                column: "FloorID",
                principalSchema: "Altinay",
                principalTable: "AppBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
