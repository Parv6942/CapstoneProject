using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Trucker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";


        [Required]
        [StringLength(20)]
        public string TruckerId { get; set; }
        public decimal TotalSpent { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
