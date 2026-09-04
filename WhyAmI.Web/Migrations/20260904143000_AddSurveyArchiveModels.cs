using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyArchiveModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SubTitle = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SurveyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyScoreOptions",
                columns: table => new
                {
                    SurveyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyScoreOptions", x => new { x.SurveyId, x.Id });
                    table.CheckConstraint("CK_SurveyScoreOptions_Score", "\"Score\" >= 1 AND \"Score\" <= 5");
                    table.ForeignKey(
                        name: "FK_SurveyScoreOptions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SurveyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SurveyTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SurveyDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ScoreOptionsSnapshot = table.Column<string>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    TotalScore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponseAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SurveyResponseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SourceQuestionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    QuestionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    AnswerText = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AnsweredAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponseAnswers", x => x.Id);
                    table.CheckConstraint("CK_SurveyResponseAnswers_Score", "\"Score\" >= 1 AND \"Score\" <= 5");
                    table.ForeignKey(
                        name: "FK_SurveyResponseAnswers_SurveyQuestions_SourceQuestionId",
                        column: x => x.SourceQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SurveyResponseAnswers_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId_Number",
                table: "SurveyQuestions",
                columns: new[] { "SurveyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyScoreOptions_SurveyId_Score",
                table: "SurveyScoreOptions",
                columns: new[] { "SurveyId", "Score" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_CustomerId",
                table: "SurveyResponses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseAnswers_SourceQuestionId",
                table: "SurveyResponseAnswers",
                column: "SourceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseAnswers_SurveyResponseId_QuestionNumber",
                table: "SurveyResponseAnswers",
                columns: new[] { "SurveyResponseId", "QuestionNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SurveyResponseAnswers");
            migrationBuilder.DropTable(name: "SurveyScoreOptions");
            migrationBuilder.DropTable(name: "SurveyQuestions");
            migrationBuilder.DropTable(name: "SurveyResponses");
            migrationBuilder.DropTable(name: "Surveys");
        }
    }
}
