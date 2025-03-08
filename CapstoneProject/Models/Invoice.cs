using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int TruckerId { get; set; }
        public Trucker Trucker { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime InvoiceDate { get; set; }
    }

}
