using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestionnaireInRazorPage.Services;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class CompleteModel(
    IQuestionCatalog catalog,
    QuestionnaireSessionStore store) : PageModel
{
    public IReadOnlyList<(QuestionExample Question, string Answer)> Responses { get; private set; } = [];

    public IActionResult OnGet()
    {
        var state = store.GetOrCreate();
        if (!state.IsComplete)
        {
            return RedirectToPage("Questionnaire");
        }

        Responses =
        [
            .. catalog.Questions
                .Select((question, index) => (question, state.Answers.GetValueOrDefault(index) ?? string.Empty))
        ];

        return Page();
    }

    public IActionResult OnPostRestart()
    {
        store.Clear();
        return RedirectToPage("Questionnaire");
    }
}
