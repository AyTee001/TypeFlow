using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTypingSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TypingSessions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TypingSessions");

            migrationBuilder.RenameColumn(
                name: "WordsTyped",
                table: "TypingSessions",
                newName: "WordsPerMinute");

            migrationBuilder.RenameColumn(
                name: "TotalCharactersTyped",
                table: "TypingSessions",
                newName: "FinishedInSeconds");

            migrationBuilder.AddColumn<float>(
                name: "Accuracy",
                table: "TypingSessions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "CharactersCount",
                table: "TypingSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CharactersPerMinute",
                table: "TypingSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "TypingSessions");

            migrationBuilder.DropColumn(
                name: "CharactersCount",
                table: "TypingSessions");

            migrationBuilder.DropColumn(
                name: "CharactersPerMinute",
                table: "TypingSessions");

            migrationBuilder.RenameColumn(
                name: "WordsPerMinute",
                table: "TypingSessions",
                newName: "WordsTyped");

            migrationBuilder.RenameColumn(
                name: "FinishedInSeconds",
                table: "TypingSessions",
                newName: "TotalCharactersTyped");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TypingSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "TypingSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
