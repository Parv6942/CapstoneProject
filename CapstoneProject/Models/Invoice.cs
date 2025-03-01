using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        public string ItemsPurchased { get; set; } // JSON string or related table

        [Required]
        public string AssignedDriver { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Unpaid"; // Default status
    }
}
