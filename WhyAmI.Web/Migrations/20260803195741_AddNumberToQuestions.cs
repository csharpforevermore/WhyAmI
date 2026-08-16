using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhyAmI.Web.Admin.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberToQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Questions");
        }
    }
}
