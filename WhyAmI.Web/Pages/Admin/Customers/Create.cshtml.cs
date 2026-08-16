using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Customers;

public class CreateModel(AppDbContext context) : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Customer Customer { get; set; } = null!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Customers.Add(Customer);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
