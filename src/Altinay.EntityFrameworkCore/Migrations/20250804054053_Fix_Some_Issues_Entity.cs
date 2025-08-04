using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altinay.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Some_Issues_Entity : Migration
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
