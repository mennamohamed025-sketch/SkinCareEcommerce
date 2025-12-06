using System.ComponentModel.DataAnnotations;

namespace SkincareEcommerce.Models
{
    public class Admin : Person
    {
        [StringLength(100)]
        public string Role { get; set; } = "Admin";
    }
}
