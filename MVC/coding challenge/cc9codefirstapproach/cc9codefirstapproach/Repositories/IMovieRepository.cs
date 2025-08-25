using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cc9codefirstapproach.Models;
using System.Collections.Generic;

namespace cc9codefirstapproach.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
        IEnumerable<Movie> GetMoviesByYear(int year);
        IEnumerable<Movie> GetMoviesByDirector(string directorName);
        void Save();
    }
}