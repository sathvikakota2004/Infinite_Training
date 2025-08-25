using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cc9codefirstapproach.Models;
using System.Collections.Generic;
using System.Data.Entity;
namespace cc9codefirstapproach.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MoviesDbContext context = new MoviesDbContext();

        public IEnumerable<Movie> GetAllMovies() => context.Movies.ToList();

        public Movie GetMovieById(int id) => context.Movies.Find(id);

        public void AddMovie(Movie movie) => context.Movies.Add(movie);

        public void UpdateMovie(Movie movie) => context.Entry(movie).State = EntityState.Modified;

        public void DeleteMovie(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie != null) context.Movies.Remove(movie);
        }

        public IEnumerable<Movie> GetMoviesByYear(int year) => context.Movies.Where(m => m.DateofRelease.Year == year).ToList();

        public IEnumerable<Movie> GetMoviesByDirector(string directorName) => context.Movies.Where(m => m.DirectorName == directorName).ToList();

        public void Save() => context.SaveChanges();
    }
}