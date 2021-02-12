using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        List<Movie> Movies = new List<Movie>()
        { 
            new Movie() { Id = 1, Name = "Shrek" },
            new Movie() { Id = 2, Name = "Wall-e" }
        };

        public IActionResult Index()
        {            
            return View(Movies);
        }        
    }
}
