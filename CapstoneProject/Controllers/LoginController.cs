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
                // First, check if the user exists by username
                var user = _context.AdminUsers.FirstOrDefault(u => u.Username == model.Username);
                if (user == null)
                {
                    ModelState.AddModelError("Username", "The username does not exist.");
                }
                else if (user.Password != model.Password)
                {
                    ModelState.AddModelError("Password", "The password is incorrect.");
                }
                else
                {
                    // Credentials verified, store the username in session and redirect
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Menu", "Home");
                }
            }
            return View(model);
        }
    }
}
