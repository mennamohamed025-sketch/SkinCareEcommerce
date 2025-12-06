using System.ComponentModel.DataAnnotations;

namespace SkincareEcommerce.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual Order Order { get; set; }
    }
}
