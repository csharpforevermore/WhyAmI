using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Customers;

public class EditModel(AppDbContext context) : PageModel
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
        Customer = customer;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        context.Attach(Customer).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(Customer.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool CustomerExists(Guid id)
    {
        return context.Customers.Any(e => e.Id == id);
    }
}
