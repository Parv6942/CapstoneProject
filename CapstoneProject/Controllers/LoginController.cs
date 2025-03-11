using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CapstoneProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly TruckerDbContext _context;

        public LoginController(TruckerDbContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Query the database for a matching admin user
                var user = _context.AdminUsers
                    .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Credentials verified, store the username in session
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Menu", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(model);
        }
    }
}
