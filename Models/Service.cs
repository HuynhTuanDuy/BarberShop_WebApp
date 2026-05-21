using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopWeb.Models
{
    [Table("SERVICES")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SERVICEID")]
        [Display(Name = "Mã DV")]
        public int ServiceID { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("SERVICENAME")] 
        public string ServiceName { get; set; }

        [Column("PRICE")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public decimal Price { get; set; }
    }
}