using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

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
            var customers = await _context.Customers
                .Include(c => c.MembershipType)
                .ToListAsync();

            return View(customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == id)
                ?.Include(c => c.MembershipType)
                .SingleOrDefaultAsync();

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public async Task<IActionResult> New()
        {
            var membershipTypes = await _context.MembershipTypes.ToListAsync();
            var newCustomer = new NewCustomerViewModel { MembershipTypes = membershipTypes };

            return View(newCustomer);
        }

        public async Task<IActionResult> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Customers");
        }
    }
}
