using System;
using System.Collections.Generic;

namespace CapstoneProject.Models
{
    public class Order
    {
        public int Id { get; set; } // ✅ Primary Key
        public int TruckerId { get; set; }
        public Trucker Trucker { get; set; } // ✅ Relationship with Trucker
        public List<OrderItem> Items { get; set; } // ✅ Order Items List
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}