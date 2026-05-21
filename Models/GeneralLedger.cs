using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShopWeb.Models
{
    [Table("GENERAL_LEDGER")]
    public class GeneralLedger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        [MaxLength(10)]
        public string TransType { get; set; } // THU hoặc CHI

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransDate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Description { get; set; }

        public int UserID { get; set; }
    }
}