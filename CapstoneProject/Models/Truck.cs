using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }

        // For example, a truck number or license plate
        public string TruckNumber { get; set; }

        // Foreign key to Trucker
        public int TruckerId { get; set; }
        public Trucker Trucker { get; set; }
    }
}
