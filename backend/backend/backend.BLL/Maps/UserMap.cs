﻿using System.Collections.Generic;
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
        private readonly IGameLogicService _gameLogicService;

        public UserMap(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ICountryService countryService, IGameLogicService gameLogicService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _countryService = countryService;
            _gameLogicService = gameLogicService;
        }

        public string Invalid { get { return "INVALID"; } }

        public string Ok { get { return "OK"; } }

        public async Task<object[]> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                return new object[] { GenerateJwtToken(model.Username, appUser), this.Ok };
            }

            return new object[] { result.ToString(), this.Invalid };
        }

        public async Task<object[]> Register(RegisterDto model)
        {
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
                    this.CreateReleatedCountry(user, model.CountryName);
                } catch (Exception e)
                {
                    await _signInManager.SignOutAsync();
                    await _userManager.DeleteAsync(user);
                    return new object[] { e.Message, this.Invalid };
                }
                // just for test
                //_gameLogicService.testMethod();
                return new object[] { token, this.Ok };
            }

            return new object[] { result.Errors.Select(x => x.Description), this.Invalid };
        }

        public async Task<object[]> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
            } catch (Exception e)
            {
                return new object[] { e.Message, this.Invalid };
            }

            return new object[] { "Signed out", this.Ok };
            
        }

        private void CreateReleatedCountry(User user, string countryName)
        {
            var country = new Country
            {
                User = user,
                CountryName = countryName

            };
            this._countryService.InsertElement(country);
        }

        private string GenerateJwtToken(string username, User user)
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

        //private string[] CollectErrorResults()
    }
}
