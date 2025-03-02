using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; } // Foreign key reference to the Item model

        [Required]
        public int Quantity { get; set; }
    }
}
