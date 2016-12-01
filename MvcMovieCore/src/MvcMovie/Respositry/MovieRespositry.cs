using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Respositry
{
    public class MovieRespositry : IMovieRespositry
    {
        public string GetMovies()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
