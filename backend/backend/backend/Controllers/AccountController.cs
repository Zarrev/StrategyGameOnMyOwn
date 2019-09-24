using System.Threading.Tasks;
using backend.BLL.Maps.Interfaces;
using backend.Model.Frontend.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace backend.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserMap _userMap;

        public AccountController(IUserMap userMap)
        {
            this._userMap = userMap;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var task = await this._userMap.Login(model);

            if (task[1].Equals(_userMap.Ok))
            {
                return Ok(new { token = task[0] });
            }

            return BadRequest(new { error = task[0]});
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var task = await this._userMap.Register(model);

            if (task[1].Equals(_userMap.Ok))
            {
                return Ok(new { token = task[0] });
            }

            return BadRequest(new { error = task[0] });
        }
    }
}