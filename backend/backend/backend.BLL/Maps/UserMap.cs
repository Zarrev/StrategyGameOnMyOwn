using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.BLL.Maps.Interfaces;
using backend.Model;
using backend.Model.Frontend.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using backend.BLL.Services.Interfaces;
using backend.BLL.Classes;

namespace backend.BLL.Maps
{
    public class UserMap : IUserMap
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICountryService _countryService;
        //private readonly IGameLogicService _gameLogicService;

        public UserMap(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ICountryService countryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _countryService = countryService;
            //_gameLogicService = gameLogicService;
        }

        public string Invalid { get { return "INVALID"; } }

        public string Ok { get { return "OK"; } }

        public async Task<UserResponseContainer> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return new UserResponseContainer { Result = new List<string> { GenerateJwtToken(model.Username, appUser) }, Validity = this.Ok };
            }

            return new UserResponseContainer { Result = new List<string> { "The username or/and password is invalid." }, Validity = this.Invalid };
        }

        public async Task<UserResponseContainer> Register(RegisterDto model)
        {
            var errors = new List<string>();
            if (model.Password != model.RepeatedPassword)
            {
                errors.Add("The password mismatched with the confirmation password");
            }
            var user = new User
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var token = GenerateJwtToken(model.Username, user);
                try
                {
                    await this.CreateReleatedCountry(user, model.CountryName);
                } catch (Exception e)
                {
                    await _signInManager.SignOutAsync();
                    await _userManager.DeleteAsync(user);
                    errors.Add(e.Message);
                }
                // just for test
                //_gameLogicService.testMethod();
                return new UserResponseContainer { Result = new List<string> { token }, Validity = this.Ok };
            }

            return new UserResponseContainer { Result = errors.Concat(result.Errors.Select(x => x.Description)).ToList() , Validity = this.Invalid };
        }

        public async Task<UserResponseContainer> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
            } catch (Exception e)
            {
                return new UserResponseContainer { Result = new List<string> { e.Message }, Validity = this.Invalid };
            }

            return new UserResponseContainer { Result = new List<string> { "Signed out" }, Validity = this.Ok };
            
        }

        private async Task CreateReleatedCountry(User user, string countryName)
        {
            var country = new Country
            {
                User = user,
                CountryName = countryName

            };
            await this._countryService.InsertElement(country);
        }

        private string GenerateJwtToken(string username, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
