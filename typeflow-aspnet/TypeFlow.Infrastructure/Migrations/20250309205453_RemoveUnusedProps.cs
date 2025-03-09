using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "TypingChallenges");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "TypingChallenges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
