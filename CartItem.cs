using System.ComponentModel.DataAnnotations;

namespace SkincareEcommerce.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
