using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TutoringRequest.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResetTokenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ResetTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetTokens_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "ModifiedById", "RoleName" },
                values: new object[,]
                {
                    { new Guid("0e9d9cc1-3aba-4209-8a90-b24fa55c99c8"), null, null, "Student" },
                    { new Guid("55ed9d2c-049a-44f9-8f69-cda2e90de7b8"), null, null, "Tutor" },
                    { new Guid("cf3e7e23-4974-43f0-8a2c-b6e8e83fccf8"), null, null, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResetTokens_AccountId",
                table: "ResetTokens",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetTokens");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0e9d9cc1-3aba-4209-8a90-b24fa55c99c8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55ed9d2c-049a-44f9-8f69-cda2e90de7b8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf3e7e23-4974-43f0-8a2c-b6e8e83fccf8"));

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
    }
}
