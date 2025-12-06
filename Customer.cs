using System.ComponentModel.DataAnnotations;

namespace SkincareEcommerce.Models
{
    public class Customer : Person
    {
        [StringLength(200)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual Cart Cart { get; set; }
    }
}
