using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.DAL;
using backend.Model;
using backend.Model.Frontend.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace backend.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;

        // TODO: not releated code move to the mapper

        public AccountController(
            
            )
        {
            
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return await GenerateJwtToken(model.Username, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            
            var user = new User
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var token = await GenerateJwtToken(model.Username, user);
                var country = new Country
                {
                    User = user
                };
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();

                return token;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }
    }
}