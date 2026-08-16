using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhyAmI.Web.Data;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Pages.Admin.Vendors;

public class DetailsModel(AppDbContext context) : PageModel
{
    public Vendor Vendor { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var vendor = await context.Vendors.FirstOrDefaultAsync(m => m.Id == id);
        if (vendor is null)
        {
            return NotFound();
        }
        else
        {
            Vendor = vendor;
        }

        return Page();
    }
}
