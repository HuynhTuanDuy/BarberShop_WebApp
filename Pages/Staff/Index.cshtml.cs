using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;

namespace BarberShop.Pages.Staff;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<User> Users { get; set; } = default!;

    public async Task OnGetAsync()
    {
        // Lấy danh sách tất cả nhân viên/quản lý từ bảng Users
        Users = await _context.Users.ToListAsync();
    }
}