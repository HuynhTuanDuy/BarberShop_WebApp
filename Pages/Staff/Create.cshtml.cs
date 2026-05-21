using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarberShopWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

namespace BarberShop.Pages.Staff;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment; 

    
    public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public User UserObj { get; set; } = default!;

    // Biến để hứng file ảnh người dùng tải lên
    [BindProperty]
    public IFormFile? AvatarFile { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Xử lý nếu người dùng có chọn file ảnh
        if (AvatarFile != null && AvatarFile.Length > 0)
        {
            // 1. Tạo thư mục 'uploads/avatars' trong wwwroot nếu chưa có
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 2. Tạo tên file độc nhất để không bị trùng (Guid + tên file gốc)
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(AvatarFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 3. Copy file vào thư mục đó
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await AvatarFile.CopyToAsync(fileStream);
            }

            // 4. Gán đường dẫn vào trường Avatar của UserObj để lưu xuống Database
            UserObj.Avatar = "/uploads/avatars/" + uniqueFileName;
        }

        // Thêm vào DB và lưu
        _context.Users.Add(UserObj);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}