using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Controllers
{
    public class TruckController : Controller
    {
        private readonly TruckerDbContext _context;

        public TruckController(TruckerDbContext context)
        {
            _context = context;
        }

        public ActionResult InputTrucker()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputTrucker(string firstName, string lastName, string truckerId)
        {
            var trucker = _context.Truckers
                .FirstOrDefault(t => t.FirstName == firstName && t.LastName == lastName && t.TruckerId == truckerId);

            if (trucker == null)
            {
                ModelState.AddModelError("", "Trucker not found.");
                return View();
            }

            return RedirectToAction("SelectItems", "Item", new { truckerId = trucker.Id });
        }
        public ActionResult TruckerSummary(int truckerId)
        {
            var trucker = _context.Truckers
                .Include(t => t.Transactions)
                .ThenInclude(tr => tr.Item)
                .FirstOrDefault(t => t.Id == truckerId);

            if (trucker == null)
            {
                return RedirectToAction("InputTrucker");
            }

            return View(trucker);
        }
        public ActionResult TruckerList()
        {
            // Fetch all truckers
            var truckers = _context.Truckers.ToList(); 
            return View(truckers);
        }
        [HttpPost]
        public IActionResult ClearTransactions(int truckerId)
        {
            var trucker = _context.Truckers
                .Include(t => t.Transactions)
                .FirstOrDefault(t => t.Id == truckerId);

            if (trucker != null && trucker.Transactions.Any())
            {
                // Remove all transactions related to the trucker
                _context.Transactions.RemoveRange(trucker.Transactions);

                // Reset the trucker's total spent
                trucker.TotalSpent = 0;

                _context.SaveChanges();
            }

            return RedirectToAction("TruckerSummary", new { truckerId });
        }
        // GET: AddTrucker
        [HttpGet]
        public ActionResult AddTrucker()
        {
            return View();
        }

        // POST: AddTrucker
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrucker(string firstName, string lastName, string truckerId)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(truckerId))
            {
                ModelState.AddModelError("", "All fields are required.");
                return View();
            }

            if (_context.Truckers.Any(t => t.TruckerId == truckerId))
            {
                ModelState.AddModelError("", "A trucker with this ID already exists.");
                return View();
            }

            var newTrucker = new Trucker
            {
                FirstName = firstName,
                LastName = lastName,
                TruckerId = truckerId
            };

            _context.Truckers.Add(newTrucker);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "New trucker added successfully!";
            return RedirectToAction("TruckerList");
        }
    }
}
