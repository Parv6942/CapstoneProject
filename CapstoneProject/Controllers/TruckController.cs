using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Data;
using CapstoneProject.Models;
using System.Collections.Generic;

namespace CapstoneProject.Controllers
{
    public class TruckController : Controller
    {
        private readonly TruckerDbContext _context;

        public TruckController(TruckerDbContext context)
        {
            _context = context;
        }

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
        {
            return View();
        }

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
        }
    }
}
