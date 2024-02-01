using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutoringRequest.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentNumberAddedToTutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "Tutors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Tutors");
        }
    }
}
