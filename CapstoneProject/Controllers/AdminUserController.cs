using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Data;
using CapstoneProject.Models;

namespace CapstoneProject.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly TruckerDbContext _context;

        public AdminUserController(TruckerDbContext context)
        {
            _context = context;
        }

        // GET: AdminUser
        public IActionResult Index()
        {
            var adminUsers = _context.AdminUsers.ToList();
            return View(adminUsers);
        }

        // GET: AdminUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                _context.AdminUsers.Add(adminUser);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(adminUser);
        }

        // GET: AdminUser/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var adminUser = _context.AdminUsers.Find(id);
            if (adminUser == null)
                return NotFound();

            return View(adminUser);
        }

        // POST: AdminUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AdminUser adminUser)
        {
            if (id != adminUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminUser);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.AdminUsers.Any(e => e.Id == adminUser.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index", "AdminUser");

            }
            return View(adminUser);
        }

        // GET: AdminUser/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var adminUser = _context.AdminUsers.FirstOrDefault(u => u.Id == id);
            if (adminUser == null)
                return NotFound();

            return View(adminUser);
        }

        // POST: AdminUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var adminUser = _context.AdminUsers.Find(id);
            _context.AdminUsers.Remove(adminUser);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
