using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace CapstoneProject.Controllers
{
    public class ItemController : Controller
    {
        private readonly TruckerDbContext _context;

        public ItemController(TruckerDbContext context)
        {
            _context = context;
        }

        public IActionResult SelectItems()
        {
            var items = _context.Items.ToList();
            var truckers = _context.Truckers.ToList();
            var cartItems = GetCartItems();

            ViewBag.Truckers = truckers;
            ViewBag.CartItems = cartItems;

            if (TempData["Receipt"] != null)
            {
                ViewBag.Receipt = JsonConvert.DeserializeObject<dynamic>(TempData["Receipt"].ToString());
            }

            return View(items);
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null || quantity < 1) return BadRequest();

            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(c => c.Item.Id == itemId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItemViewModel { Item = item, Quantity = quantity });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int itemId, int quantity)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(c => c.Item.Id == itemId);

            if (existingItem != null)
            {
                existingItem.Quantity -= quantity;
                if (existingItem.Quantity <= 0) cart.Remove(existingItem);
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new { success = true });
        }

        public IActionResult ViewCart()
        {
            var cartItems = GetCartItems();
            return PartialView("_CartPartial", cartItems);
        }

        public IActionResult GetTotalPrice()
        {
            var cart = GetCartItems();
            decimal totalPrice = cart.Sum(item => item.Item.Price * item.Quantity);
            return Json(new { totalPrice = totalPrice.ToString("0.00") });
        }

        public IActionResult ViewReceipt()
        {
            var cartItems = GetCartItems();
            if (cartItems == null || !cartItems.Any()) return Content("<p>No items in the cart.</p>");

            decimal totalPrice = cartItems.Sum(item => item.Item.Price * item.Quantity);

            string receiptHtml = "<ul>";
            foreach (var item in cartItems)
            {
                receiptHtml += $"<li>{item.Item.Name} x {item.Quantity} - ${(item.Item.Price * item.Quantity).ToString("0.00")}</li>";
            }
            receiptHtml += $"</ul><p><strong>Total:</strong> ${totalPrice.ToString("0.00")}</p>";

            return Content(receiptHtml);
        }

        [HttpPost]
        public IActionResult Invoice(int truckerId)
        {
            var trucker = _context.Truckers.FirstOrDefault(t => t.Id == truckerId);
            var cartItems = GetCartItems();

            if (trucker == null || cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Invalid request.";
                return RedirectToAction("SelectItems");
            }

            decimal totalPrice = cartItems.Sum(item => item.Item.Price * item.Quantity);

            var invoiceViewModel = new InvoiceViewModel
            {
                Trucker = trucker,
                CartItems = cartItems,
                TotalPrice = totalPrice
            };

            // Clear cart after generating invoice
            HttpContext.Session.Remove("Cart");

            return View("Invoice", invoiceViewModel); // Render the Invoice view with the model
        }


        private List<CartItemViewModel> GetCartItems()
        {
            return HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
        }
    }
}
