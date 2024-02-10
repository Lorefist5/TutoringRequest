using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TutoringRequest.Data.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StudentNumber = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LastLogIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MajorName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvailabilitySlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TutorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailabilitySlots_Accounts_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseName = table.Column<string>(type: "TEXT", nullable: false),
                    MajorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RolesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutoringSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TutorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutoringSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutoringSections_Accounts_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutoringSections_Accounts_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutoringSections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "ModifiedById", "RoleName" },
                values: new object[,]
                {
                    { new Guid("70acc156-27c3-4890-afb3-254d2f468091"), null, null, "Admin" },
                    { new Guid("e7247af7-65f5-42d6-bc45-2ac21803123e"), null, null, "Tutor" },
                    { new Guid("e9c30ad9-ff45-49ea-ae85-f64db8bb6bdf"), null, null, "Student" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesId",
                table: "AccountRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_TutorId",
                table: "AvailabilitySlots",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorId",
                table: "Courses",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSections_CourseId",
                table: "TutoringSections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSections_StudentId",
                table: "TutoringSections",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSections_TutorId",
                table: "TutoringSections",
                column: "TutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "AvailabilitySlots");

            migrationBuilder.DropTable(
                name: "TutoringSections");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
