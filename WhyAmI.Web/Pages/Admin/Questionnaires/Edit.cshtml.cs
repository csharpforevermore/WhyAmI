using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Questionnaires;

public class EditModel(AppDbContext context) : PageModel
{
    [BindProperty]
    public Questionnaire Questionnaire { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var questionnaire = await context.Questionnaires
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (questionnaire is null)
        {
            return NotFound();
        }

        Questionnaire = questionnaire;
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

        var questionnaire = await context.Questionnaires
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.Id == Questionnaire.Id);

        if (questionnaire is null)
        {
            return NotFound();
        }

        questionnaire.Title = Questionnaire.Title;
        questionnaire.ScoreOptions =
        [
            .. Questionnaire.ScoreOptions
                .Select(o => new ScoreOption { Score = o.Score, Text = o.Text })
        ];
        questionnaire.HasQuestionsWithTrueFalse = Questionnaire.HasQuestionsWithTrueFalse;

        SyncQuestions(questionnaire, Questionnaire.Questions);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuestionnaireExists(Questionnaire.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private void SyncQuestions(Questionnaire tracked, IList<Question> posted)
    {
        var postedById = posted
            .Where(q => q.Id != Guid.Empty)
            .ToDictionary(q => q.Id);

        var toRemove = tracked.Questions
            .Where(existing => !postedById.ContainsKey(existing.Id))
            .ToList();

        foreach (var question in toRemove)
        {
            tracked.Questions.Remove(question);
            context.Questions.Remove(question);
        }

        foreach (var postedQuestion in posted)
        {
            if (postedQuestion.Id != Guid.Empty &&
                tracked.Questions.FirstOrDefault(q => q.Id == postedQuestion.Id) is { } existing)
            {
                existing.Text = postedQuestion.Text;
                existing.Number = postedQuestion.Number;
                continue;
            }

            // Leave Id empty: a client Guid makes EF UPDATE a missing row (concurrency exception).
            tracked.Questions.Add(new Question
            {
                Text = postedQuestion.Text,
                Number = postedQuestion.Number
            });
        }
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

    private bool QuestionnaireExists(Guid id)
    {
        return context.Questionnaires.Any(e => e.Id == id);
    }
}
