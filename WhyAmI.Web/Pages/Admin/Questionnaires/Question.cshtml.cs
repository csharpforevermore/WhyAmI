using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Questionnaires;

public class QuestionModel(AppDbContext context) : PageModel
{
    public Questionnaire Questionnaire { get; set; } = null!;
    public Question Question { get; set; } = null!;
    public int CurrentIndex { get; set; }

    // Accepts either route values or query string values: ?id=...&number=... or /Questionnaires/{id}/{number}
    public async Task<IActionResult> OnGetAsync(Guid? id, int? number, Guid? questionId)
    {
        if (id is null)
        {
            return NotFound();
        }

        var questionnaire = await context.Questionnaires
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (questionnaire is null)
        {
            return NotFound();
        }

        Questionnaire = questionnaire;

        var questions = Questionnaire.Questions
            .OrderBy(q => q.Number)
            .ToList();

        if (questions.Count == 0)
        {
            return Page();
        }

        if (number.HasValue)
        {
            Question = questions.FirstOrDefault(q => q.Number == number.Value) ?? questions.First();
        }
        else if (questionId.HasValue && questionId != Guid.Empty)
        {
            Question = questions.FirstOrDefault(q => q.Id == questionId.Value) ?? questions.First();
        }
        else
        {
            Question = questions.First();
        }

        CurrentIndex = questions.FindIndex(q => q.Id == Question.Id);

        return Page();
    }
}