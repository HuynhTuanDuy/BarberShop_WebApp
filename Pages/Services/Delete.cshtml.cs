using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShop.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    // Đã đổi serviceid thành id
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var service = await _context.Services.FirstOrDefaultAsync(m => m.ServiceID == id);
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

    // Đã đổi serviceid thành id
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            Service = service;
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}