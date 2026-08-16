using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhyAmI.Web.Admin.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreValueHeadingsToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScoreValueHeadings",
                table: "Questions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreValueHeadings",
                table: "Questions");
        }
    }
}
