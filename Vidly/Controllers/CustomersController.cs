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

        public async Task<IActionResult> Form()
        {            
            var membershipTypes = await _context.MembershipTypes.ToListAsync();
            var newCustomer = new CustomerFormViewModel 
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View(newCustomer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var membershipTypes = await _context.MembershipTypes.ToListAsync();

            var formCustomer = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View("Form", formCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var membershipTypes = await _context.MembershipTypes.ToListAsync();
                var customerFormViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = membershipTypes
                };

                return View("Form", customerFormViewModel);
            }

            if (customer.Id == 0)
                await _context.Customers.AddAsync(customer);
            else
            {
                var customerInDb = await _context.Customers.SingleAsync(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Customers");
        }
    }
}
