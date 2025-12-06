using System.ComponentModel.DataAnnotations;

namespace SkincareEcommerce.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // In real app, hash this

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
