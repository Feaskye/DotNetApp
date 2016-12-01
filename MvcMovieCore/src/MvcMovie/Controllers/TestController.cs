using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private Respositry.IMovieRespositry _MovieRespositry = null;
        public TestController(Respositry.IMovieRespositry movieRespositry)
        {
            _MovieRespositry = movieRespositry;
        }

        // GET api/values/5
        [HttpGet]
        public string Get(int id)
        {
            return _MovieRespositry.GetMovies();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
