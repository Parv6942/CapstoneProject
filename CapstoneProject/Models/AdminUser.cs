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

        // Flag to indicate if this user is the top admin.
        public bool IsTopAdmin { get; set; }

    }
}
