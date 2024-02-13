using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TutoringRequest.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("70acc156-27c3-4890-afb3-254d2f468091"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e7247af7-65f5-42d6-bc45-2ac21803123e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e9c30ad9-ff45-49ea-ae85-f64db8bb6bdf"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "ModifiedById", "RoleName" },
                values: new object[,]
                {
                    { new Guid("055ecbca-854b-4cad-b47d-b51d3da3ad73"), null, null, "Tutor" },
                    { new Guid("0a8fcf80-e28a-4591-925f-e233f4ab2ebb"), null, null, "Student" },
                    { new Guid("2475a1c9-052d-4b39-a6c6-8cad2ab23690"), null, null, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("055ecbca-854b-4cad-b47d-b51d3da3ad73"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0a8fcf80-e28a-4591-925f-e233f4ab2ebb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2475a1c9-052d-4b39-a6c6-8cad2ab23690"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "ModifiedById", "RoleName" },
                values: new object[,]
                {
                    { new Guid("70acc156-27c3-4890-afb3-254d2f468091"), null, null, "Admin" },
                    { new Guid("e7247af7-65f5-42d6-bc45-2ac21803123e"), null, null, "Tutor" },
                    { new Guid("e9c30ad9-ff45-49ea-ae85-f64db8bb6bdf"), null, null, "Student" }
                });
        }
    }
}
