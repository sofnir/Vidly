using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly VidlyContext _context;
        private readonly IMapper _mapper;

        public CustomersController(VidlyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {            
            var customers = await _context.Customers.ToListAsync();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        [HttpGet("{id}")]
        public async Task<CustomerDto> GetCustomers(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                NotFound();

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        [HttpPost]
        public async Task<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {            
            var customer = _mapper.Map<Customer>(customerDto);

            if (!ModelState.IsValid)
                BadRequest();

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            customerDto.Id = customer.Id;

            return customerDto;
        }

        [HttpPut("{id}")]
        public async Task UpdateCustomer(int id, CustomerDto customerDto)
        {            
            if (!ModelState.IsValid)
                BadRequest();

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

            if (customerInDb == null)
                NotFound();

            _mapper.Map(customerDto, customerInDb);
            customerInDb.Id = id;

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
