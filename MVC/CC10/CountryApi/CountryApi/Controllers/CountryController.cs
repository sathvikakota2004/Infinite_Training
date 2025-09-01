using CountryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CountryApi.Controllers
{
    [RoutePrefix("api/countries")]
    public class CountryController : ApiController
    {
        
        private static List<Country> countries = new List<Country>
        {
            new Country { Id = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { Id = 2, CountryName = "USA",   Capital = "Washington D.C." },
            new Country { Id = 3, CountryName = "Japan", Capital = "Tokyo" },
            new Country { Id = 4, CountryName = "Germany",Capital = "Berlin" },
            new Country { Id = 5, CountryName = "Australia",Capital = "Canberra" }
        };

        
        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(countries);
        }

        
        
        
        [HttpPost, Route("")]
        public IHttpActionResult Create([FromBody] Country country)
        {
            if (country == null) return BadRequest("Invalid data.");

            country.Id = countries.Max(c => c.Id) + 1; // Auto increment
            countries.Add(country);

            return CreatedAtRoute("GetCountryById", new { id = country.Id }, country);
        }

       
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] Country country)
        {
            if (country == null || id != country.Id) return BadRequest("Invalid data.");

            var existing = countries.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.CountryName = country.CountryName;
            existing.Capital = country.Capital;

            return Ok(existing);
        }

        
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var existing = countries.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            countries.Remove(existing);
            return StatusCode(HttpStatusCode.NoContent); // 204
        }
    }
}