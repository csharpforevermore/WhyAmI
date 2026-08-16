using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhyAmI.Web.Models;
using WhyAmI.Web.Services;

namespace WhyAmI.Web.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class QuestionnaireModel(
    IQuestionCatalog catalog,
    QuestionnaireSessionStore store) : PageModel
{
    [BindProperty]
    public string? Answer { get; set; }

    public QuestionExample CurrentQuestion { get; private set; } = null!;
    public int StepNumber { get; private set; }
    public int TotalSteps => catalog.Questions.Count;
    public bool CanGoBack { get; private set; }

    public IActionResult OnGet()
    {
        var state = store.GetOrCreate();
        if (state.IsComplete)
        {
            return RedirectToPage("Complete");
        }

        Bind(state);
        return Page();
    }

    public IActionResult OnPostNext()
    {
        var state = store.GetOrCreate();
        if (state.IsComplete)
        {
            return RedirectToPage("Complete");
        }

        if (string.IsNullOrWhiteSpace(Answer))
        {
            ModelState.AddModelError(nameof(Answer), "Please answer before continuing.");
            Bind(state);
            return Page();
        }

        state.Answers[state.CurrentIndex] = Answer.Trim();

        if (state.CurrentIndex >= catalog.Questions.Count - 1)
        {
            state.IsComplete = true;
            store.Save(state);
            return RedirectToPage("Complete");
        }

        state.CurrentIndex++;
        store.Save(state);
        return RedirectToPage();
    }

    public IActionResult OnPostPrevious()
    {
        var state = store.GetOrCreate();
        if (state.IsComplete)
        {
            return RedirectToPage("Complete");
        }

        if (state.CurrentIndex > 0)
        {
            state.CurrentIndex--;
            store.Save(state);
        }

        return RedirectToPage();
    }

    private void Bind(QuestionnaireState state)
    {
        StepNumber = state.CurrentIndex + 1;
        CurrentQuestion = catalog.Questions[state.CurrentIndex];
        CanGoBack = state.CurrentIndex > 0;
        Answer = state.Answers.GetValueOrDefault(state.CurrentIndex);
    }
}
