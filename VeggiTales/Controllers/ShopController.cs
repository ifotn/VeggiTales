using Microsoft.AspNetCore.Mvc;
using VeggiTales.Models;

namespace VeggiTales.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
            var products = new List<Product>();

            for (int i = 1; i < 11; i++)
            {
                products.Add(new Product { ProductId = i, Name = "Product " + i.ToString() });
            }

            // pass the data to the view for display
            return View(products);
        }
    }
}
