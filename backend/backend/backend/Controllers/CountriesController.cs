using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using backend.Model.Frontend;
using backend.BLL.Maps.Interfaces;
using backend.BLL.Services.Interfaces;

namespace backend.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryMap _countryMap;
        private readonly IRoundService _roundService;

        public CountriesController(ICountryMap countryMap, IRoundService roundService)
        {
            _countryMap = countryMap;
            _roundService = roundService;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryView>>> Getcountries()
        {
            return await _countryMap.GetAll();
        }

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

        [HttpGet("mycountry")]
        public async Task<ActionResult<CountryView>> GetCountryForUser()
        {
            var id = HttpContext.User.Identity.Name;
            var country = await _countryMap.GetElementByUser(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

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

        [HttpGet("mycountry/inhabitant")]
        public async Task<ActionResult> GetInhabitant()
        {
            return Ok(new { inhabitant = await _countryMap.GetInhabitant(HttpContext.User.Identity.Name) });
        }

        [HttpGet("mycountry/pearl")]
        public async Task<ActionResult> GetPearlNumber()
        {
            return Ok(new { pearl = await _countryMap.GetPearlNumber(HttpContext.User.Identity.Name) });
        }

        [HttpGet("mycountry/buildings")]
        public async Task<ActionResult> GetBuildingsNumber()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(new
            {
                flowControllNumber = await _countryMap.GetFlowControllerNumber(userId),
                reefCastleNumber = await _countryMap.GetReefCastleNumber(userId)
            });
        }

        [HttpGet("mycountry/mercenaries")]
        public async Task<ActionResult> GetMercenariesNumber()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(new
            {
                seaDogNumber = await _countryMap.GetAssaultSeaDogNumber(userId),
                battleSeahorse = await _countryMap.GetBattleSeahorseNumber(userId),
                laserShark = await _countryMap.GetLaserSharkNumber(userId)
            });
        }

        [HttpGet("mycountry/points")]
        public async Task<ActionResult> GetPoints()
        {
            return Ok(new { points = await _countryMap.GetPoints(HttpContext.User.Identity.Name) });
        }

        [HttpGet("mycountry/rank")]
        public async Task<ActionResult> GetRank()
        {
            return Ok(new { rank = await _countryMap.GetRank(HttpContext.User.Identity.Name) });
        }
        [HttpGet("mycountry/developments")]
        public async Task<ActionResult> GetDevelopments()
        {
            var userId = HttpContext.User.Identity.Name;
            var developments = await _countryMap.GetDevelopments(userId);
            return Ok(new
            {
                MudTractor = developments[0],
                Sludgeharvester = developments[1],
                CoralWall = developments[2],
                SonarGun = developments[3],
                UnderwaterMaterialArts = developments[4],
                Alchemy = developments[5]
            });
        }

        [HttpGet("mycountry/attack")]
        public async Task<ActionResult> GetAttackValue()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(new
            {
                attack = await _countryMap.GetAttackValue(userId)
            });
        }

        [HttpGet("mycountry/defense")]
        public async Task<ActionResult> GetDefenseValue()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(new
            {
                attack = await _countryMap.GetDefenseValue(userId)
            });
        }

        [HttpGet("next")]
        public async Task<ActionResult> FireNextRound()
        {
            var userId = HttpContext.User.Identity.Name;
            return Ok(new
            {
                country = await _countryMap.FireNextRound(userId),
                round = await _roundService.IncrementRound()
            });
        }

        [HttpGet("round")]
        public async Task<ActionResult<int>> GetActualRound()
        {
            return _roundService.ServerRoundNumber;
        }

        [HttpGet("mycountry/devround")]
        public async Task<ActionResult<int>> GetDevRound()
        {
            var userId = HttpContext.User.Identity.Name;
            return await _countryMap.GetDevRound(userId);
        }

        [HttpGet("mycountry/buildround")]
        public async Task<ActionResult<int>> GetBuildRound()
        {
            var userId = HttpContext.User.Identity.Name;
            return await _countryMap.GetBuildRound(userId);
        }

        [HttpGet("mycountry/buildingname")]
        public async Task<ActionResult> GetBuildingName()
        {
            return Ok(new { buildingname = await _countryMap.GetBuildingName(HttpContext.User.Identity.Name) });
        }

        [HttpGet("mycountry/developingname")]
        public async Task<ActionResult> GetDevelopingName()
        {
            return Ok(new { developingname = await _countryMap.GetDevelopingName(HttpContext.User.Identity.Name) });
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry([FromRoute] string id, [FromBody] CountryView country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            await _countryMap.Update(country);

            return Ok();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<CountryView>> PostCountry(CountryView country)
        {
            await _countryMap.Create(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }


        [HttpPost("mycountry/build")]
        public async Task<ActionResult> PostBuildAction([FromBody] int buildingType)
        {
            await _countryMap.Build(HttpContext.User.Identity.Name, buildingType);

            return Ok();
        }

        [HttpPost("mycountry/develop")]
        public async Task<ActionResult> PostDevelopAction([FromBody] int developType)
        {
            await _countryMap.Develop(HttpContext.User.Identity.Name, developType);

            return Ok();
        }

        [HttpPost("mycountry/hire")]
        public async Task<ActionResult> PostHireAction(MercenaryRequest mercanryList)
        {
            var message = await _countryMap.HireMercenary(HttpContext.User.Identity.Name, mercanryList);

            if (message.Equals("OK"))
            {
                return Ok(new { message });
            }

            return BadRequest(new { error = message });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryView>> DeleteCountry(string id)
        {
            var country = await _countryMap.GetElement(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countryMap.Delete(id);

            return country;
        }


    }
}
