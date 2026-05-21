using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace BarberShop.Pages.Staff;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public User UserObj { get; set; } = default!;

    [BindProperty]
    public IFormFile? AvatarFile { get; set; }

    // Biến mới: Nhận biết người dùng có tích chọn Xóa ảnh hay không
    [BindProperty]
    public bool DeleteAvatar { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var user = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
        if (user == null) return NotFound();

        UserObj = user;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("UserObj.PasswordHash");

        if (!ModelState.IsValid) return Page();

        var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserID == UserObj.UserID);
        if (userToUpdate == null) return NotFound();

        userToUpdate.FullName = UserObj.FullName;
        userToUpdate.UserName = UserObj.UserName;
        userToUpdate.Role = UserObj.Role;
        userToUpdate.IsActive = UserObj.IsActive;

        // XỬ LÝ ẢNH ĐẠI DIỆN
        if (AvatarFile != null && AvatarFile.Length > 0)
        {
            // Tình huống 1: Người dùng tải ảnh mới lên (Ưu tiên cao nhất)
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(AvatarFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await AvatarFile.CopyToAsync(fileStream);
            }

            // Xóa ảnh cũ
            if (!string.IsNullOrEmpty(userToUpdate.Avatar) && userToUpdate.Avatar.StartsWith("/uploads/"))
            {
                var oldFilePath = Path.Combine(_environment.WebRootPath, userToUpdate.Avatar.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
            }

            userToUpdate.Avatar = "/uploads/avatars/" + uniqueFileName;
        }
        else if (DeleteAvatar)
        {
            // Tình huống 2: Không tải ảnh mới, nhưng TÍCH CHỌN xóa ảnh cũ
            if (!string.IsNullOrEmpty(userToUpdate.Avatar) && userToUpdate.Avatar.StartsWith("/uploads/"))
            {
                var oldFilePath = Path.Combine(_environment.WebRootPath, userToUpdate.Avatar.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
            }

            // Xóa đường dẫn trong cơ sở dữ liệu
            userToUpdate.Avatar = null;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}