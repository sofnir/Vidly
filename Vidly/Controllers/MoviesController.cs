using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

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
            return View();
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

        public async Task<IActionResult> Form(int id)
        {
            var genres = await _context.Genres.ToListAsync();
            var movieFormViewModel = new MovieFormViewModel();

            if (id != 0)
            {
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

                if (movie == null)
                    return NotFound();

                movieFormViewModel = new MovieFormViewModel(movie);                
            }            

            movieFormViewModel.Genres = genres;

            return View(movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var genres = await _context.Genres.ToListAsync();
                var movieFormViewModel = new MovieFormViewModel(movie)
                {
                    Genres = genres
                };

                return View("Form", movieFormViewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                await _context.Movies.AddAsync(movie);
            }                
            else
            {
                var movieInDb = await _context.Movies.SingleAsync(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Movies");
        }
    }
}
