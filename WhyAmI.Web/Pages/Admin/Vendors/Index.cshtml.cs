using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Vendors;

public class IndexModel(AppDbContext context) : PageModel
{
    public IList<Vendor> Vendor { get; set; } = null!;

    public async Task OnGetAsync()
    {
        Vendor = await context.Vendors.ToListAsync();
    }
}
