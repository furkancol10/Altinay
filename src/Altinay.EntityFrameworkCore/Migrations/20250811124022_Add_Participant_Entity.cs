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
            migrationBuilder.CreateTable(
                name: "AppParticipant",
                schema: "Altinay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppParticipant_AppBooking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "Altinay",
                        principalTable: "AppBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppParticipant_BookingID",
                schema: "Altinay",
                table: "AppParticipant",
                column: "BookingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppParticipant",
                schema: "Altinay");
        }
    }
}
