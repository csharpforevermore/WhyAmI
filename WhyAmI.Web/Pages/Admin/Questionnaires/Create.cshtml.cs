using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Questionnaires;

public class CreateModel(AppDbContext context) : PageModel
{
    [BindProperty]
    public Questionnaire Questionnaire { get; set; } = null!;

    public IActionResult OnGet()
    {
        Questionnaire = new Questionnaire
        {
            Title = string.Empty,
            ScoreOptions = [new ScoreOption { Score = 0, Text = string.Empty }],
            Questions =
            [
                new Question { Text = string.Empty }
            ]
        };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        NormalizePostedData();

        if (!ValidateQuestionnaire())
        {
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        foreach (var question in Questionnaire.Questions)
        {
            if (question.Id == Guid.Empty)
            {
                question.Id = Guid.NewGuid();
            }
        }

        context.Questionnaires.Add(Questionnaire);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void NormalizePostedData()
    {
        Questionnaire.Questions = [.. Questionnaire.Questions];

        Questionnaire.ScoreOptions = [.. Questionnaire.ScoreOptions];
    }

    private bool ValidateQuestionnaire()
    {
        if (Questionnaire.ScoreOptions.Count == 0)
        {
            ModelState.AddModelError("Questionnaire.ScoreOptions", "At least one score option is required.");
        }

        if (Questionnaire.Questions.Count == 0)
        {
            ModelState.AddModelError("Questionnaire.Questions", "At least one question is required.");
            return ModelState.IsValid;
        }

        for (var i = 0; i < Questionnaire.Questions.Count; i++)
        {
            var question = Questionnaire.Questions[i];
            if (string.IsNullOrWhiteSpace(question.Text))
            {
                ModelState.AddModelError($"Questionnaire.Questions[{i}].Text", "Question text is required.");
            }
        }

        return ModelState.IsValid;
    }
}
