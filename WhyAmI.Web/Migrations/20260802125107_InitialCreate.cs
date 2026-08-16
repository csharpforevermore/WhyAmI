using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhyAmI.Web.Admin.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AlternatePhoneNumber = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContactDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    About = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Website = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ContactDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendors_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ScoreOptions = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionnaireId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_QuestionnaireId",
                        column: x => x.QuestionnaireId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuestionnaireResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalScore = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuestionnaireResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuestionnaireResults_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerCustomerQuestionnaireResults",
                columns: table => new
                {
                    AnswersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerQuestionnaireResultsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerCustomerQuestionnaireResults", x => new { x.AnswersId, x.CustomerQuestionnaireResultsId });
                    table.ForeignKey(
                        name: "FK_AnswerCustomerQuestionnaireResults_Answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerCustomerQuestionnaireResults_CustomerQuestionnaireResults_CustomerQuestionnaireResultsId",
                        column: x => x.CustomerQuestionnaireResultsId,
                        principalTable: "CustomerQuestionnaireResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuestionnaireResultsQuestion",
                columns: table => new
                {
                    CustomerQuestionnaireResultsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuestionnaireResultsQuestion", x => new { x.CustomerQuestionnaireResultsId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_CustomerQuestionnaireResultsQuestion_CustomerQuestionnaireResults_CustomerQuestionnaireResultsId",
                        column: x => x.CustomerQuestionnaireResultsId,
                        principalTable: "CustomerQuestionnaireResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerQuestionnaireResultsQuestion_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuestionnaireResultsVendor",
                columns: table => new
                {
                    CustomerQuestionnaireResultsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VendorsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuestionnaireResultsVendor", x => new { x.CustomerQuestionnaireResultsId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_CustomerQuestionnaireResultsVendor_CustomerQuestionnaireResults_CustomerQuestionnaireResultsId",
                        column: x => x.CustomerQuestionnaireResultsId,
                        principalTable: "CustomerQuestionnaireResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerQuestionnaireResultsVendor_Vendors_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCustomerQuestionnaireResults_CustomerQuestionnaireResultsId",
                table: "AnswerCustomerQuestionnaireResults",
                column: "CustomerQuestionnaireResultsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuestionnaireResults_CustomerId",
                table: "CustomerQuestionnaireResults",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuestionnaireResultsQuestion_QuestionsId",
                table: "CustomerQuestionnaireResultsQuestion",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuestionnaireResultsVendor_VendorsId",
                table: "CustomerQuestionnaireResultsVendor",
                column: "VendorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactDetailsId",
                table: "Customers",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnaireId",
                table: "Questions",
                column: "QuestionnaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ContactDetailsId",
                table: "Vendors",
                column: "ContactDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerCustomerQuestionnaireResults");

            migrationBuilder.DropTable(
                name: "CustomerQuestionnaireResultsQuestion");

            migrationBuilder.DropTable(
                name: "CustomerQuestionnaireResultsVendor");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "CustomerQuestionnaireResults");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ContactDetails");
        }
    }
}
