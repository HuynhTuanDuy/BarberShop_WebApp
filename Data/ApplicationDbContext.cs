using Microsoft.EntityFrameworkCore;

namespace BarberShopWeb.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Khai báo các tập hợp dữ liệu (DbSet) đại diện cho các bảng
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<GeneralLedger> GeneralLedgers { get; set; }
    }
}