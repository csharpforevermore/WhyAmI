using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Questionnaires;

public class IndexModel(AppDbContext context) : PageModel
{
    public IList<Questionnaire> Questionnaire { get; set; } = null!;

    public async Task OnGetAsync()
    {
        Questionnaire = await context.Questionnaires
            .Include(q => q.Questions)
            .ToListAsync();
    }
}
