using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
        public int Quantity { get; set; }
    }
}
