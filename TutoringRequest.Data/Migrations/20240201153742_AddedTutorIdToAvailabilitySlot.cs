using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutoringRequest.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTutorIdToAvailabilitySlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots");

            migrationBuilder.AlterColumn<Guid>(
                name: "TutorId",
                table: "AvailabilitySlots",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots");

            migrationBuilder.AlterColumn<Guid>(
                name: "TutorId",
                table: "AvailabilitySlots",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlots_Tutors_TutorId",
                table: "AvailabilitySlots",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id");
        }
    }
}
