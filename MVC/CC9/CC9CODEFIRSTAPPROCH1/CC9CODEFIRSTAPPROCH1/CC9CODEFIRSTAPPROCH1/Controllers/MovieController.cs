using CC9CODEFIRSTAPPROCH1.Models;
using CC9CODEFIRSTAPPROCH1.Respository;
using System.Web.Mvc;

namespace CC9CODEFIRSTAPPROCH1.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repo = new MovieRepository();

        // GET: /Movie/Index
        public ActionResult Index()
        {
            var movies = repo.GetAll();
            return Json(movies, JsonRequestBehavior.AllowGet); // Return JSON instead of view
        }

        // GET: /Movie/Create?name=Avengers&director=Russo&date=2025-01-01
        public ActionResult Create(string name, string director, string date)
        {
            var movie = new Movies
            {
                MovieName = name,
                DirectorName = director,
                DateOfRelease = System.DateTime.Parse(date)
            };
            repo.Add(movie);
            return Json(new { success = true, movie }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Movie/Edit?id=1&name=NewName
        public ActionResult Edit(int id, string name, string director, string date)
        {
            var movie = repo.GetById(id);
            if (movie != null)
            {
                movie.MovieName = name ?? movie.MovieName;
                movie.DirectorName = director ?? movie.DirectorName;
                if (!string.IsNullOrEmpty(date)) movie.DateOfRelease = System.DateTime.Parse(date);
                repo.Update(movie);
            }
            return Json(new { success = true, movie }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Movie/Delete?id=1
        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Movie/MoviesByYear?year=2025
        public ActionResult MoviesByYear(int year)
        {
            var movies = repo.GetByYear(year);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }

        // GET: /Movie/MoviesByDirector?director=Steven
        public ActionResult MoviesByDirector(string director)
        {
            var movies = repo.GetByDirector(director);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}
