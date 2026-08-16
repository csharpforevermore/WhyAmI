using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuestionnaireShowScoreOptionTextAsHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowScoreOptionTextAsHeader",
                table: "Questionnaires");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowScoreOptionTextAsHeader",
                table: "Questionnaires",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
