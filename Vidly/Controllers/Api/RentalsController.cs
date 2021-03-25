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
    public class RentalsController : ControllerBase
    {
        private readonly VidlyContext _context;        

        public RentalsController(VidlyContext context)
        {
            _context = context;            
        }

        [HttpPost]
        public async Task<ActionResult> NewRental(NewRentalDto newRentalDto)
        {
            var customer = await _context.Customers.SingleAsync(
                c => c.Id == newRentalDto.CustomerId);

            var movies = _context.Movies.Where(
                m => newRentalDto.MovieIds.Contains(m.Id));            

            foreach (var movie in movies)
            {
                var newRental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                await _context.AddAsync(newRental);
            }
            
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
