using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Questionnaires;

public class DeleteModel(AppDbContext context) : PageModel
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var questionnaire = await context.Questionnaires.FindAsync(id);
        if (questionnaire != null)
        {
            Questionnaire = questionnaire;
            context.Questionnaires.Remove(Questionnaire);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
