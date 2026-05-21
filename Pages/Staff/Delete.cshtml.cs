using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace BarberShop.Pages.Staff;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public DeleteModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public User UserObj { get; set; } = default!;

    public string ErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var user = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
        if (user == null) return NotFound();

        UserObj = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        var userToDelete = await _context.Users.FindAsync(id);

        if (userToDelete != null)
        {
            try
            {
                // 1. Nếu nhân viên có ảnh đại diện, xóa ảnh thực tế trên máy chủ
                if (!string.IsNullOrEmpty(userToDelete.Avatar) && userToDelete.Avatar.StartsWith("/uploads/"))
                {
                    var filePath = Path.Combine(_environment.WebRootPath, userToDelete.Avatar.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                // 2. Xóa dữ liệu trong Oracle Database
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                // Bắt lỗi: Nếu nhân viên này đã từng thực hiện hóa đơn (có liên kết khóa ngoại), DB sẽ chặn không cho xóa.
                // Trả về giao diện kèm thông báo lỗi thay vì làm sập trang web.
                UserObj = userToDelete;
                ErrorMessage = "Không thể xóa hồ sơ này vì nhân viên đã có dữ liệu hóa đơn (Order) lưu trong hệ thống. Vui lòng chuyển trạng thái sang 'Tạm nghỉ' thay vì xóa.";
                return Page();
            }
        }

        return RedirectToPage("./Index");
    }
}