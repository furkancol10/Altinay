using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Add_Participant_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participants",
                schema: "Altinay",
                table: "AppBooking",
                newName: "Description");

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

            migrationBuilder.AddColumn<bool>(
                name: "AllDay",
                schema: "Altinay",
                table: "AppBooking",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppParticipant",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppParticipant_AppBooking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "Altinay",
                        principalTable: "AppBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppParticipant_BookingId",
                schema: "Altinay",
                table: "AppParticipant",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppParticipant",
                schema: "Altinay");

            migrationBuilder.DropColumn(
                name: "AllDay",
                schema: "Altinay",
                table: "AppBooking");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Altinay",
                table: "AppBooking",
                newName: "Participants");

            migrationBuilder.AlterColumn<string>(
                name: "BookedBy",
                schema: "Altinay",
                table: "AppBooking",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
