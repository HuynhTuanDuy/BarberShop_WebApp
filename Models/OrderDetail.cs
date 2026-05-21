using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopWeb.Models
{
    [Table("ORDERDETAILS")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailID { get; set; }

        public int OrderID { get; set; }
        public int ServiceID { get; set; }
        public int UserID { get; set; }

        public decimal ServicePrice { get; set; }
        public decimal Commission { get; set; }
    }
}