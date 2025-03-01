using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    public class LoginController : Controller
    {
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
                // Replace this with real authentication logic (e.g., database check)
                if (model.Username == "Admin" && model.Password == "Password123")
                {
                    // Redirect to the Menu action in the HomeController
                    return RedirectToAction("Menu", "Home");
                }
                else
                {
                    // Add an error message for invalid login
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View(model);
        }
    }
}
