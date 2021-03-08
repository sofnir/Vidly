using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly VidlyContext _context;

        public CustomersController(VidlyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {            
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        [HttpGet("{id}")]
        public async Task<Customer> GetCustomers(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                NotFound();

            return customer;
        }

        [HttpPost]
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                BadRequest();

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        [HttpPut("{id}")]
        public async Task UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                NotFound();

            customerInDb.Name = customer.Name;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCustomer(int id)
        {            
            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                NotFound();

            _context.Customers.Remove(customerInDb);
            await _context.SaveChangesAsync();
        }
    }
}
