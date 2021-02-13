using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Data
{
    public class DbInitializer
    {
        public static void Initialize(VidlyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer() { Name = "John Smith" },
                new Customer() { Name = "Mary Williams" }
            };

            foreach (Customer customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();            

            var movies = new Movie[]
            {
                new Movie() { Name = "Shrek" },
                new Movie() { Name = "Wall-e" }
            };

            foreach (Movie movie in movies)
            {
                context.Movies.Add(movie);
            }

            context.SaveChanges();
        }
    }
}
