﻿using Microsoft.AspNetCore.Mvc;
using VeggiTales.Data;
using VeggiTales.Models;

namespace VeggiTales.Controllers
{
    public class ShopController : Controller
    {
        // class level db object
        private readonly ApplicationDbContext _context;

        // constructor using db dependency
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // use Category model to fetch categories
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();

            return View(categories);
        }

        // GET: /Shop/ByCategory/{name}
        public IActionResult ByCategory(string name)
        {
            if (name == null)
            {
                return RedirectToAction("Index");
            }

            // use ViewData dictionary to pass the category name to the view for display
            ViewData["Category"] = name;

            // use the Product model to make a mock list of products for display
            // this is strongly-typed data so we won't use ViewData
            //var products = new List<Product>();

            //for (int i = 1; i < 16; i++)
            //{
            //    products.Add(new Product { ProductId = i, Name = "Product " + i.ToString(), Price = 10 + i });
            //}

            // fetch products from the db
            var products = _context.Products
                 .OrderBy(p => p.Name)
                 .Where(p => p.Category.Name == name)             
                .ToList();

            // pass the data to the view for display
            return View(products);
        }

        // POST: /Shop/AddToCart => process request to add an item to the user's cart
        [HttpPost]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            // look up product & get price
            var product = _context.Products.Find(ProductId);    

            if (product == null) { 
                return NotFound();
            }

            // figure out the cart owner
            var customerId = GetCustomerId();

            // check if product is already in this user's cart
            var cartItem = _context.CartItems
                .Where(c => c.ProductId == ProductId && c.CustomerId == customerId)
                .FirstOrDefault();

            if (cartItem == null)
            {
                // create a new product object
                cartItem = new CartItem
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    CustomerId = customerId,
                    Price = product.Price
                };
               
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += Quantity;
                _context.CartItems.Update(cartItem);
            }

             // save to db
            _context.SaveChanges();

            // redirect to cart page
            return RedirectToAction("Cart");
        }

        // GET: /Shop/Cart => display current user's cart
        public IActionResult Cart()
        {
            // identify the cart using the session var
            var customerId = GetCustomerId();

            // fetch the items in this cart
            var cartItems = _context.CartItems.Where(c => c.CustomerId == customerId);  

            return View(cartItems);
        }

        public string GetCustomerId()
        {
            // check for existing session var for this user
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                // if there is no session var, create one using a GUID
                HttpContext.Session.SetString("CustomerId", Guid.NewGuid().ToString());
            }

            // return the session var
            return HttpContext.Session.GetString("CustomerId");
        }
    }
}
