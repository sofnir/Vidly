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
    public class MoviesController : Controller
    {
        private readonly VidlyContext _context;

        public MoviesController(VidlyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movies = await _context.Movies
                .Where(c => c.Id == id)
                ?.Include(m => m.Genre)
                .SingleOrDefaultAsync();

            if (movies == null)
                return NotFound();

            return View(movies);
        }
    }
}
