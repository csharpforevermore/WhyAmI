using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Migrations
{
    /// <inheritdoc />
    public partial class MoveScoreValueHeadingsToQuestionnaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreValueHeadings",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "ScoreValueHeadings",
                table: "Questionnaires",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreValueHeadings",
                table: "Questionnaires");

            migrationBuilder.AddColumn<string>(
                name: "ScoreValueHeadings",
                table: "Questions",
                type: "TEXT",
                nullable: true);
        }
    }
}
