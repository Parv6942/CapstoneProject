using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var item = _context.Items
                .AsNoTracking() // ✅ Ensures a fresh copy of the item is retrieved
                .FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                var existingItem = cart.FirstOrDefault(c => c.Item != null && c.Item.Id == itemId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        Item = new Item
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Price = item.Price,
                            ImageUrl = item.ImageUrl // ✅ Ensures all properties are stored
                        },
                        Quantity = quantity
                    });
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult SetTrucker(int truckerId)
        {
            var trucker = _context.Truckers.FirstOrDefault(t => t.Id == truckerId);

            if (trucker != null)
            {
                HttpContext.Session.SetInt32("SelectedTruckerId", truckerId);
            }

            return RedirectToAction("Checkout"); 
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int itemId)
        {
            var cart = GetCartItems();
            cart.RemoveAll(c => c.ItemId == itemId);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return Ok();
        }

        public IActionResult ViewReceipt()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            if (!cart.Any())
            {
                return Content("<p>Your cart is empty.</p>", "text/html");
            }

            string receiptHtml = "<table class='table'><tr><th>Item</th><th>Quantity</th><th>Price</th><th>Total</th></tr>";

            foreach (var cartItem in cart)
            {
                if (cartItem.Item != null)
                {
                    receiptHtml += $"<tr><td>{cartItem.Item.Name}</td><td>{cartItem.Quantity}</td><td>${cartItem.Price}</td><td>${cartItem.TotalPrice}</td></tr>";
                }
            }

            receiptHtml += "</table>";

            return Content(receiptHtml, "text/html");
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

            decimal totalPrice = cartItems.Sum(item => item.TotalPrice);

            var invoiceViewModel = new InvoiceViewModel
            {
                Trucker = trucker,
                CartItems = cartItems,
                TotalPrice = totalPrice
            };

            return View("Invoice", invoiceViewModel);
        }
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var selectedTruckerId = HttpContext.Session.GetInt32("SelectedTruckerId");

            var trucker = _context.Truckers.FirstOrDefault(t => t.Id == selectedTruckerId);
            ViewBag.SelectedTrucker = trucker; // ✅ Pass trucker to view

            return View(cart); // ✅ Pass List<CartItem> instead of List<CartItemViewModel>
        }

        [HttpPost]
        public IActionResult ConfirmOrder()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var selectedTruckerId = HttpContext.Session.GetInt32("SelectedTruckerId");

            if (!cart.Any() || selectedTruckerId == null)
            {
                TempData["Error"] = "Invalid order request.";
                return RedirectToAction("SelectItems");
            }

            var trucker = _context.Truckers.FirstOrDefault(t => t.Id == selectedTruckerId);
            if (trucker == null)
            {
                TempData["Error"] = "Selected Trucker not found.";
                return RedirectToAction("SelectItems");
            }

            // ✅ Save Order to Database
            var order = new Order
            {
                TruckerId = trucker.Id,
                Items = cart.Select(c => new OrderItem
                {
                    ItemId = c.Item.Id,
                    Quantity = c.Quantity,
                    Price = c.Item.Price
                }).ToList(),
                TotalPrice = cart.Sum(c => c.Item.Price * c.Quantity),
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges(); // ✅ Save Order

            // ✅ Create Invoice
            var invoice = new Invoice
            {
                OrderId = order.Id,
                Trucker = trucker,
                Order = order,
                InvoiceDate = DateTime.Now,
                TotalPrice = order.TotalPrice
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges(); // ✅ Save Invoice

            HttpContext.Session.Remove("Cart"); // ✅ Clear cart after checkout

            return RedirectToAction("ViewInvoice", "Invoice", new { id = invoice.Id });
        }

        public IActionResult ViewInvoice(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Trucker)
                .Include(i => i.CartItems)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
            {
                TempData["Error"] = "Invoice not found.";
                return RedirectToAction("SelectItems");
            }

            return View(invoice);
        }


        private List<CartItemViewModel> GetCartItems()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            return cart;
        }
    }
}
