using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShop.Pages.ServicePages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? serviceid)
    {
        if (serviceid is null)
        {
            return NotFound();
        }

        var service = await _context.Services.FindAsync(serviceid);
        if (service != null)
        {
            Service = service;
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
