using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.BLL.Maps.Interfaces;
using backend.DAL;
using backend.Model;
using backend.Model.Frontend.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace backend.API.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserMap _userMap;

        public AccountController(IUserMap userMap)
        {
            this._userMap = userMap;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            return await this._userMap.Login(model);
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            return await this._userMap.Register(model);
        }
    }
}