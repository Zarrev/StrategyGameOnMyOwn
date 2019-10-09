using System.Collections.Generic;
using System.Threading.Tasks;
using backend.BLL.Classes;
using backend.BLL.Maps.Interfaces;
using backend.Model.Frontend;
using backend.Model.Frontend.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            _userMap = userMap;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserView>>> GetUsers()
        {
            return await this._userMap.GetUsers();
        }

        [HttpGet("{searchText}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserView>>> GetUsersBySearch(string searchText)
        {
            if (searchText is null)
            {
                throw new System.ArgumentNullException(nameof(searchText));
            }

            return await this._userMap.GetUsersBySearchWithPoints(searchText);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            return SendRespons(await _userMap.Login(model));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            return SendRespons(await _userMap.Register(model));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogOut()
        {
            return SendRespons(await _userMap.LogOut());
        }

        private List<string> CollectErrorMessages()
        {
            var errorMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
            }

            return errorMessages;
        }

        private ObjectResult SendRespons(UserResponseContainer userResponseContainer)
        {
            var errorMessages = CollectErrorMessages();

            if (userResponseContainer.Validity.Equals(_userMap.Ok))
            {
                return Ok(new { message = userResponseContainer.Result[0] });
            }

            errorMessages.AddRange(userResponseContainer.Result);
            return BadRequest(new { error = errorMessages.ToArray() });
        }
    }
}