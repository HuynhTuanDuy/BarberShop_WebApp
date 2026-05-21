using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShop.Pages.Services;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Service Service { get; set; } = default!;

    // Đã đổi từ serviceid thành id để khớp hoàn toàn với Frontend
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
}