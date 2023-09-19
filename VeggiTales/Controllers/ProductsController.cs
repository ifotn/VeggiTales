using Microsoft.AspNetCore.Mvc;

namespace VeggiTales.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /products/create => show empty Product form to create a new record
        public IActionResult Create()
        {
            return View();
        }

        // GET: /products/edit/5 => show Product form
        public IActionResult Edit(int id) 
        {
            return View();
        }
    }
}
