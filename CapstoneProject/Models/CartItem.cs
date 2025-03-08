<<<<<<< HEAD
﻿namespace CapstoneProject.Models
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TruckerId { get; set; }
=======
﻿using System.ComponentModel.DataAnnotations;

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
>>>>>>> Emmanuel
    }
}
