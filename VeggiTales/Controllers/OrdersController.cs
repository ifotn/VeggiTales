using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeggiTales.Data;
using VeggiTales.Models;

namespace VeggiTales.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return _context.Orders != null ? 
                    View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            else
            {
                // filter list for logged in customer only
                var orders = await _context.Orders.Where(o => o.CustomerId == User.Identity.Name).ToListAsync();
                return View(orders);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Administrator"))
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .FirstOrDefaultAsync(m => m.OrderId == id);
                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }
            else // customer can only see their own orders
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => o.CustomerId == User.Identity.Name)
                    .FirstOrDefaultAsync(m => m.OrderId == id);
                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }
                
        }      
    }
}
