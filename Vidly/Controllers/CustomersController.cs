using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        List<Customer> Customers = new List<Customer>()
        {
            new Customer() { Id = 1, Name = "John Smith" },
            new Customer() { Id = 2, Name = "Mary Williams" }
        };

        public IActionResult Index()
        {            
            return View(Customers);
        }

        public IActionResult Details(int id)
        {
            var customer = Customers?.Where(q => q.Id == id)?.FirstOrDefault();

            if (customer == null)
                return NotFound();

            return Content(customer.Name);
        }
    }
}
