using Microsoft.AspNetCore.Mvc;

namespace VeggiTales.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
