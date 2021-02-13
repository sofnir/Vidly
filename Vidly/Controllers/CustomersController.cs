using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VidlyContext _context;

        public CustomersController(VidlyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.Customers.Where(q => 
                q.Id == id)?.SingleOrDefaultAsync();

            if (customer == null)
                return NotFound();

            return Content(customer.Name);
        }
    }
}
