using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhyAmI.Web.Admin.Migrations
{
    /// <inheritdoc />
    public partial class AddSubtitleAndShowSummaryOfHeadersText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowScoreOptionTextAsHeader",
                table: "Questionnaires",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Questionnaires",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowScoreOptionTextAsHeader",
                table: "Questionnaires");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Questionnaires");
        }
    }
}
