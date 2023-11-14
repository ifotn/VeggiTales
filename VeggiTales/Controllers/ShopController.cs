using Microsoft.AspNetCore.Mvc;
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

            // figure out the cart owner

            // create a new product object

            // save to db

            // redirect to cart page
            return RedirectToAction("Cart");
        }
    }
}
