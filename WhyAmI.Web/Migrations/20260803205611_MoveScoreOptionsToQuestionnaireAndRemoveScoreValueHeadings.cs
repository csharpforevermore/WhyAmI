using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Migrations
{
    /// <inheritdoc />
    public partial class MoveScoreOptionsToQuestionnaireAndRemoveScoreValueHeadings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreOptions",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ScoreValueHeadings",
                table: "Questionnaires");

            migrationBuilder.AddColumn<string>(
                name: "ScoreOptions",
                table: "Questionnaires",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreOptions",
                table: "Questionnaires");

            migrationBuilder.AddColumn<string>(
                name: "ScoreOptions",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScoreValueHeadings",
                table: "Questionnaires",
                type: "TEXT",
                nullable: true);
        }
    }
}
