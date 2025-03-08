<<<<<<< HEAD
﻿namespace CapstoneProject.Models
{
    public class CartItemViewModel
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
=======
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Models
{
    [NotMapped] // ✅ Prevents EF from treating this as a database entity
    public class CartItemViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
>>>>>>> Emmanuel
    }
}
