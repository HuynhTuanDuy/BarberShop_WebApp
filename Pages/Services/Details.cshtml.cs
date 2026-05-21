using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShop.Pages.ServicePages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Service Service { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? serviceid)
    {
        if (serviceid is null)
        {
            return NotFound();
        }

        var service = await _context.Services.FirstOrDefaultAsync(m => m.ServiceID == serviceid);
        if (service is null)
        {
            return NotFound();
        }
        else
        {
            Service = service;
        }

        return Page();
    }
}
