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
    public class BattleController : ControllerBase
    {
        private readonly IBattleMap _battleMap;

        public BattleController(IBattleMap battleMap)
        {
            _battleMap = battleMap;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BattleView>>> GetBattles()
        {
            return await _battleMap.GetAllByUserId(HttpContext.User.Identity.Name);
        }

        // POST
        [HttpPost("/attack")]
        public async Task<IActionResult> PostBattle([FromBody] BattleView battle)
        {
            await _battleMap.Create(battle, HttpContext.User.Identity.Name);
            return Ok();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> PostBattlel( BattleView battle)
        {
            await _battleMap.Create(battle, HttpContext.User.Identity.Name);
            return Ok();
        }
    }
}