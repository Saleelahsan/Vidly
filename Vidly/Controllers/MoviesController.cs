using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private Datacontext _context;

        public MoviesController()
        {
            _context = new Datacontext();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };

            var Customers = new List<Customer>
            {
                new Customer{Name="Customer 1"},
                new Customer{Name="Customer 2"}
            };


            var ViewModel = new RandomMovieViewModel
            { Movie = movie, Customers = Customers
            };


            return View(ViewModel);

            //return View(movie);

            //return Content("Hello World...!!");

            //return HttpNotFound();

            //return new EmptyResult();

            //return RedirectToAction("Index", "Home", new { page = "1", sortby = "name" });
        }


        public ActionResult Edit(int Id)
        {

            var movie = _context.Movie.SingleOrDefault(c => c.Id == Id);

            if(movie==null)
            {
                return HttpNotFound();
            }

            var viewmodel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };


            return View("MovieForm", viewmodel);

        }

        public ActionResult Index(int? pageIndex,string sortBy)
        {
            if(!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if(string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }

            return Content(string.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        
        [Route("movies/released/{year}/{month:regex(\\d{2})}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        public ActionResult ViewMovies()
        {
            var Movies = _context.Movie.Include(c => c.Genre).ToList();


            //var moviesList = new List<Movie> { new Movie { Name = "Shrek" }, new Movie { Name = "Hulk" } };

            //var movies = new MoviesList { Movies = moviesList };

            

            

            return View(Movies);
        }

        public ActionResult Details(int Id)
        {
            var Movie = _context.Movie.Include(c => c.Genre).SingleOrDefault(c => c.Id == Id);

            if (Movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(Movie);
            }
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewmodel = new MovieFormViewModel
            {
                Genres = genres
            };


            return View("MovieForm",viewmodel);
        }

        public ActionResult Save(Movie movie)
        {

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;

                _context.Movie.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movie.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();


            return RedirectToAction("ViewMovies", "Movies");
        }
    }
}