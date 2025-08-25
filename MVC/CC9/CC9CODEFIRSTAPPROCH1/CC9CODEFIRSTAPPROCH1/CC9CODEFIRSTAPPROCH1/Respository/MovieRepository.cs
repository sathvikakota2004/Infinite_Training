using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC9CODEFIRSTAPPROCH1.Models;
using System.Collections.Generic;



namespace CC9CODEFIRSTAPPROCH1.Respository
{
    
        public class MovieRepository : IMovieRepository
        {
            private MoviesDbContext db = new MoviesDbContext();

            public IEnumerable<Movies> GetAll()
            {
                return db.Movies.ToList();
            }

            public Movies GetById(int id)
            {
                return db.Movies.Find(id);
            }

            public void Add(Movies movie)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }

            public void Update(Movies movie)
            {
                var existing = db.Movies.Find(movie.Mid);
                if (existing != null)
                {
                    existing.MovieName = movie.MovieName;
                    existing.DirectorName = movie.DirectorName;
                    existing.DateOfRelease = movie.DateOfRelease;
                    db.SaveChanges();
                }
            }

            public void Delete(int id)
            {
                var movie = db.Movies.Find(id);
                if (movie != null)
                {
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                }
            }

            public IEnumerable<Movies> GetByYear(int year)
            {
                return db.Movies.Where(m => m.DateOfRelease.Year == year).ToList();
            }

            public IEnumerable<Movies> GetByDirector(string directorName)
            {
                return db.Movies.Where(m => m.DirectorName == directorName).ToList();
            }
        }
    }
