using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Vendors;

public class CreateModel(AppDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Vendor Vendor { get; set; } = null!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Vendors.Add(Vendor);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
