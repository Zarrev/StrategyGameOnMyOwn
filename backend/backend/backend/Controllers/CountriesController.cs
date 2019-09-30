using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using backend.Model.Frontend;
using backend.BLL.Maps.Interfaces;

namespace backend.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryMap _countryMap;

        public CountriesController(ICountryMap countryMap)
        {
            _countryMap = countryMap;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryView>>> Getcountries()
        {
            return await _countryMap.GetAll();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryView>> GetCountry(string id)
        {
            var country = await _countryMap.GetElement(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // GET: api/Countries/5
        [HttpGet("byuser/{userId}")]
        public async Task<ActionResult<CountryView>> GetCountryByUser(string userId)
        {
            var country = await _countryMap.GetElementByUser(userId);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry([FromRoute] string id, [FromBody] CountryView country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _countryMap.Update(country);

            return Ok();
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<CountryView>> PostCountry(CountryView country)
        {
            _countryMap.Create(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryView>> DeleteCountry(string id)
        {
            var country = await _countryMap.GetElement(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryMap.Delete(id);

            return country;
        }
    }
}
