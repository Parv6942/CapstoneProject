using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // Optionally, assign roles (e.g., "admin", "manager", etc.)
        public string Role { get; set; }
    }
}
