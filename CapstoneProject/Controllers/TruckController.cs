<<<<<<< HEAD
﻿using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
=======
﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Data;
using CapstoneProject.Models;
using System.Collections.Generic;
>>>>>>> Emmanuel

namespace CapstoneProject.Controllers
{
    public class TruckController : Controller
    {
        private readonly TruckerDbContext _context;

        public TruckController(TruckerDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
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
=======
        // GET: Truck
        public async Task<IActionResult> Index()
        {
            var truckers = await _context.Truckers.Include(t => t.Trucks).ToListAsync();
            return View(truckers);
        }

        // GET: Truck/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var trucker = await _context.Truckers
                .Include(t => t.Trucks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trucker == null)
                return NotFound();

            return View(trucker);
        }

        // GET: Truck/Create
        public IActionResult Create()
>>>>>>> Emmanuel
        {
            return View();
        }

<<<<<<< HEAD
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
=======
        // POST: Truck/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trucker trucker, List<string> truckNumbers)
        {
            if (ModelState.IsValid)
            {
                // Add trucks if any truck numbers were provided
                if (truckNumbers != null && truckNumbers.Any())
                {
                    foreach (var truckNumber in truckNumbers)
                    {
                        if (!string.IsNullOrWhiteSpace(truckNumber))
                        {
                            trucker.Trucks.Add(new Truck { TruckNumber = truckNumber });
                        }
                    }
                }

                _context.Add(trucker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trucker);
        }

        // GET: Truck/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trucker = await _context.Truckers
                .Include(t => t.Trucks)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trucker == null)
                return NotFound();

            return View(trucker);
        }

        // POST: Truck/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trucker trucker, List<string> truckNumbers)
        {
            if (id != trucker.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Update basic trucker details
                    _context.Update(trucker);

                    // Remove any existing trucks for this trucker
                    var existingTrucks = _context.Trucks.Where(t => t.TruckerId == trucker.Id);
                    _context.Trucks.RemoveRange(existingTrucks);

                    // Add updated truck numbers (if provided)
                    if (truckNumbers != null && truckNumbers.Any())
                    {
                        foreach (var truckNumber in truckNumbers)
                        {
                            if (!string.IsNullOrWhiteSpace(truckNumber))
                            {
                                trucker.Trucks.Add(new Truck { TruckNumber = truckNumber, TruckerId = trucker.Id });
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckerExists(trucker.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trucker);
        }

        // GET: Truck/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trucker = await _context.Truckers
                .Include(t => t.Trucks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trucker == null)
                return NotFound();

            return View(trucker);
        }

        // POST: Truck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trucker = await _context.Truckers
                .Include(t => t.Trucks)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (trucker != null)
            {
                // Remove associated trucks first
                _context.Trucks.RemoveRange(trucker.Trucks);
                _context.Truckers.Remove(trucker);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TruckerExists(int id)
        {
            return _context.Truckers.Any(e => e.Id == id);
>>>>>>> Emmanuel
        }
    }
}
