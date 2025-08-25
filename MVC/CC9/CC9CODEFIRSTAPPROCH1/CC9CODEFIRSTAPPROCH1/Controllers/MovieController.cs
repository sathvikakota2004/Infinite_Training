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
            return Json(movies, JsonRequestBehavior.AllowGet); 
        }

        
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

        
        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

     
        public ActionResult MoviesByYear(int year)
        {
            var movies = repo.GetByYear(year);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult MoviesByDirector(string director)
        {
            var movies = repo.GetByDirector(director);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}
