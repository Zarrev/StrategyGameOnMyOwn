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

namespace backend.BLL.Maps
{
    public class UserMap : IUserMap
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICountryService _countryService;

        public UserMap(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ICountryService countryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _countryService = countryService;
        }

        public async Task<object> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return await GenerateJwtToken(model.Username, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public async Task<object> Register(RegisterDto model)
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
                try
                {
                    this.createReleatedCountry(user, model.CountryName);
                } catch (Exception e)
                {
                    await _signInManager.SignOutAsync();
                    await _userManager.DeleteAsync(user);
                    throw new ApplicationException("The user's country cannot be created! Error: " + e.Message);
                }

                return token;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private void createReleatedCountry(User user, string countryName)
        {
            var country = new Country
            {
                User = user,
                CountryName = countryName

            };
            this._countryService.InsertElement(country);
        }

        private async Task<object> GenerateJwtToken(string username, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
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
