using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Customers;

public class IndexModel(AppDbContext context) : PageModel
{
    public IList<Customer> Customer { get; set; } = null!;

    public async Task OnGetAsync()
    {
        Customer = await context.Customers.ToListAsync();
    }
}
