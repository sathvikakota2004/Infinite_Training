using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CC9CODEFIRSTAPPROCH1.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext() : base("name=MoviesDBConnection") { }

        public DbSet<Movies> Movies { get; set; }
    }
}