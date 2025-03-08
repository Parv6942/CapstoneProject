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

        public IActionResult GenerateInvoice(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.Trucker)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("SelectItems", "Item");
            }

            var invoice = new Invoice
            {
                OrderId = order.Id,
                TruckerId = order.TruckerId,
                TotalPrice = order.Items.Sum(oi => oi.Quantity * oi.Price), // ✅ Correct total calculation
                InvoiceDate = DateTime.Now
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("ViewInvoice", "Invoice", new { id = invoice.Id });
        }


        public IActionResult ViewInvoice(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Trucker)
                .Include(i => i.Order)
                .ThenInclude(o => o.Items)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                TempData["Error"] = "Invoice not found.";
                return RedirectToAction("SelectItems", "Item");
            }

            return View(invoice);
        }

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
