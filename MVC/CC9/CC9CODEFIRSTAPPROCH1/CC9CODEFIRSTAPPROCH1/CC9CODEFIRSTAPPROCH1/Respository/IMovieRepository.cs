using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC9CODEFIRSTAPPROCH1.Models;
using System.Collections.Generic;

namespace CC9CODEFIRSTAPPROCH1.Respository
{
    public interface IMovieRepository
    {
        IEnumerable<Movies> GetAll();
        Movies GetById(int id);
        void Add(Movies movie);
        void Update(Movies movie);
        void Delete(int id);
        IEnumerable<Movies> GetByYear(int year);
        IEnumerable<Movies> GetByDirector(string directorName);
    }
}