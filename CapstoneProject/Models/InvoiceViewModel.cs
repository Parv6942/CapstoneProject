using CapstoneProject.Models;
using System.Collections.Generic;

namespace CapstoneProject.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public Trucker Trucker { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
    }
}
