using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cc9codefirstapproach.Models;
using System.Collections.Generic;
using cc9codefirstapproach.Repositories;

namespace cc9codefirstapproach.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repo = new MovieRepository();
        // GET: Movie
        public ActionResult Index()
        {
            var movies = repo.GetAllMovies();
            return View(movies);
        }
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repo.AddMovie(movie);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = repo.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
              repo.UpdateMovie(movie);
               repo.Save();
              return RedirectToAction("Index");
            }
          return View(movie);
        }

        public ActionResult Delete(int id)
        {
                var movie = repo.GetMovieById(id);
                  return View(movie);
        }

      [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
               repo.DeleteMovie(id);
            repo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult ByYear(int year = 2010)
        {
            var movies = repo.GetMoviesByYear(year);
            return View(movies);
        }

        public ActionResult ByDirector(string directorName = "Nolan")
        {
            var movies = repo.GetMoviesByDirector(directorName);
            return View(movies);
        }
    }
}
