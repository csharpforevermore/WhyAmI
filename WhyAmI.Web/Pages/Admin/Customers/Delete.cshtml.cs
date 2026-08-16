using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Customers;

public class DeleteModel(AppDbContext context) : PageModel
{
    [BindProperty]
    public Customer Customer { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var customer = await context.Customers.FirstOrDefaultAsync(m => m.Id == id);
        if (customer is null)
        {
            return NotFound();
        }
        else
        {
            Customer = customer;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var customer = await context.Customers.FindAsync(id);
        if (customer != null)
        {
            Customer = customer;
            context.Customers.Remove(Customer);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
