using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TypingChallenges");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "TypingChallenges");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TypingChallenges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TypingChallenges",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TypingChallenges");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TypingChallenges");

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "TypingChallenges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "TypingChallenges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
