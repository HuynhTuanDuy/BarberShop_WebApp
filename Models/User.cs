using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopWeb.Models
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("USERID")]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("FULLNAME")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("USERNAME")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("PASSWORDHASH")]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ROLE")]
        public string Role { get; set; } = "NHÂN VIÊN"; // Đổi giá trị mặc định

        [Column("ISACTIVE")]
        public int IsActive { get; set; } = 1;

        // THÊM THUỘC TÍNH MỚI: AVATAR
        [Column("AVATAR")]
        public string? Avatar { get; set; } // Dấu ? nghĩa là có thể để trống (null)
    }
}