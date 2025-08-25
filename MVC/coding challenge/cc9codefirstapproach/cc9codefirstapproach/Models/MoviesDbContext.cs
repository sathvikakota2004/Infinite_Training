using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace cc9codefirstapproach.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext() : base("MoviesDBConnection")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}