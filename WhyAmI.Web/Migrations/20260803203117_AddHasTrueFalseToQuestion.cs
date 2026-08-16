using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhyAmI.Web.Admin.Migrations
{
    /// <inheritdoc />
    public partial class AddHasTrueFalseToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasTrueFalse",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasTrueFalse",
                table: "Questions");
        }
    }
}
