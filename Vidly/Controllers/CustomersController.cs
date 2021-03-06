﻿using Microsoft.AspNetCore.Mvc;
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
            return View();
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

        public async Task<IActionResult> Form(int id)
        {            
            var membershipTypes = await _context.MembershipTypes.ToListAsync();
            var customerFormViewModel = new CustomerFormViewModel();

            if(id != 0)
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                    return NotFound();

                customerFormViewModel = new CustomerFormViewModel(customer);
            }

            customerFormViewModel.MembershipTypes = membershipTypes;            

            return View(customerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var membershipTypes = await _context.MembershipTypes.ToListAsync();
                var customerFormViewModel = new CustomerFormViewModel(customer)
                {                    
                    MembershipTypes = membershipTypes
                };

                return View("Form", customerFormViewModel);
            }

            if (customer.Id == 0)
            {
                await _context.Customers.AddAsync(customer);
            }                
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
