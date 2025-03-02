using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CapstoneProject.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly TruckerDbContext _context;

        public InvoiceController(TruckerDbContext context)
        {
            _context = context;
        }

        // ✅ Display a specific invoice
        public IActionResult ViewInvoice(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Trucker)
                .Include(i => i.CartItems)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                TempData["Error"] = "Invoice not found.";
                return RedirectToAction("SelectItems", "Item");
            }

            return View(invoice);
        }

        // ✅ Show all invoices (For admin or customer history)
        public IActionResult AllInvoices()
        {
            var invoices = _context.Invoices
                .Include(i => i.Trucker)
                .OrderByDescending(i => i.InvoiceDate)
                .ToList();

            return View(invoices);
        }
    }
}
