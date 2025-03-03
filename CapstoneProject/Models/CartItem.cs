using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; } 

        [Required]
        public int Quantity { get; set; }
        public decimal Price => Item?.Price ?? 0m;

        public decimal TotalPrice => (Item?.Price ?? 0m) * Quantity;
    }
}
