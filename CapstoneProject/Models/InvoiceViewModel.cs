using CapstoneProject.Models;
using System.Collections.Generic;

namespace CapstoneProject.Models
{
    public class InvoiceViewModel
    {
        public Trucker Trucker { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
