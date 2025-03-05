using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Trucker
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        // Computed property for convenience
        public string FullName => $"{FirstName} {LastName}";



        // Navigation properties
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Truck> Trucks { get; set; } = new List<Truck>();
    }
}
