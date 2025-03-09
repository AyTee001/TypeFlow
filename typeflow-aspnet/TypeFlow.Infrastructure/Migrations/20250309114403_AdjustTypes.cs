using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypingSessions_TypingChallenges_ChallengeId",
                table: "TypingSessions");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "TypingSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_TypingSessions_TypingChallenges_ChallengeId",
                table: "TypingSessions",
                column: "ChallengeId",
                principalTable: "TypingChallenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypingSessions_TypingChallenges_ChallengeId",
                table: "TypingSessions");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "TypingSessions");

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accuracy = table.Column<float>(type: "real", nullable: false),
                    AverageCharactersPerMinute = table.Column<int>(type: "int", nullable: false),
                    AverageWordsPerMinute = table.Column<int>(type: "int", nullable: false),
                    TotalTests = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TypingSessions_TypingChallenges_ChallengeId",
                table: "TypingSessions",
                column: "ChallengeId",
                principalTable: "TypingChallenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
