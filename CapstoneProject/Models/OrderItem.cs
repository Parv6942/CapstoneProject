namespace CapstoneProject.Models
{
    public class OrderItem
    {
        public int Id { get; set; } // ✅ Primary Key
        public int OrderId { get; set; } // ✅ Foreign Key
        public Order Order { get; set; } // ✅ Relationship with Order
        public int ItemId { get; set; } // ✅ Foreign Key
        public Item Item { get; set; } // ✅ Relationship with Item
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
