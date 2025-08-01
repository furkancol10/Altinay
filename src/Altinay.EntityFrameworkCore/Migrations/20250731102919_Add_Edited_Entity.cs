using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Add_Edited_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoom_AppDevices_DevicesID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_AppRoom_AppFloor_FloorID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropTable(
                name: "AppDevices",
                schema: "Altinay");

            migrationBuilder.DropTable(
                name: "AppFloor",
                schema: "Altinay");

            migrationBuilder.DropIndex(
                name: "IX_AppRoom_DevicesID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "DevicesID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "Altinay",
                table: "AppRoom",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "Altinay",
                table: "AppRoom",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Altinay",
                table: "AppRoom",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Altinay",
                table: "AppRoom",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "Altinay",
                table: "AppBooking",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "Altinay",
                table: "AppBooking",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "Altinay",
                table: "AppBooking",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                schema: "Altinay",
                table: "AppBooking",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                schema: "Altinay",
                table: "AppBooking",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                schema: "Altinay",
                table: "AppBooking",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Altinay",
                table: "AppBooking",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "Altinay",
                table: "AppBooking",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                schema: "Altinay",
                table: "AppBooking",
                type: "uuid",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoom_AppBooking_FloorID",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Altinay",
                table: "AppRoom");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.AddColumn<Guid>(
                name: "DevicesID",
                schema: "Altinay",
                table: "AppRoom",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppDevices",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    RoomID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDevices_AppRoom_RoomId1",
                        column: x => x.RoomId1,
                        principalSchema: "Altinay",
                        principalTable: "AppRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFloor",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFloor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoom_DevicesID",
                schema: "Altinay",
                table: "AppRoom",
                column: "DevicesID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDevices_RoomId1",
                schema: "Altinay",
                table: "AppDevices",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoom_AppDevices_DevicesID",
                schema: "Altinay",
                table: "AppRoom",
                column: "DevicesID",
                principalSchema: "Altinay",
                principalTable: "AppDevices",
                principalColumn: "Id");

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
    }
}
