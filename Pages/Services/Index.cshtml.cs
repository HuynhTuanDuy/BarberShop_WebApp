using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Pages.Services;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Khai báo danh sách Services để giao diện HTML có thể đọc được
    public IList<Service> Services { get; set; } = default!;

    public async Task OnGetAsync()
    {
        // Kéo toàn bộ danh sách dịch vụ từ bảng Services trong Oracle lên
        if (_context.Services != null)
        {
            Services = await _context.Services.ToListAsync();
        }
    }
}