using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.DAL.Repository.AbstractClasses;
using backend.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using backend.BLL.Services.AbstractClasses;
using backend.BLL.Services.Interfaces;


namespace backend.BLL.Services
{
    public class UserService : AUserService
    {
        private readonly AUserRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICountryService _countryService;

        public UserService(AUserRepository repository, SignInManager<User> signInManager,
            IConfiguration configuration, ICountryService countryService)
        {
            _repository = repository;
            _signInManager = signInManager;
            _configuration = configuration;
            _countryService = countryService;
        }

        public override async Task DeleteElement(string elementId)
        {
            await _repository.DeleteElement(elementId);
            await _repository.Save();
        }

        public override async Task<User> GetElementById(string elementId)
        {
            return await _repository.GetElementById(elementId);
        }

        public override async Task<List<User>> GetElements()
        {
            return await _repository.GetElements().ContinueWith(task => (List<User>) task.Result);
        }

        public override async Task<IdentityResult> InsertElement(User element, string password)
        {
            var identityResult = await _repository.InsertElement(element, password);
            await _repository.Save();
            return identityResult;
        }

        public override Task UpdateElement(User element)
        {
            throw new NotImplementedException("Currently this function is unnecessary.");
        }

        public override async Task<List<string>> Login(string username, string password)
        {
            var signInresult = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (signInresult.Succeeded)
            {
                User existingUser = null;
                try
                {
                    existingUser = (await this.GetElements()).SingleOrDefault(r => r.UserName == username);
                    if (existingUser == null)
                    {
                        return new List<string> { this.Invalid, "The user is not exist" };
                    }
                    return new List<string> { this.Ok, this.GenerateJwtToken(existingUser) };
                }
                catch (Exception e)
                {
                    // "The user stored multiple times in the database. Please contect with an administrator!" 
                    return new List<string> { this.Invalid, e.Message };
                }
            }
            return new List<string> { this.Invalid, "The username or/and password is invalid." };
        }

        public override async Task<List<string>> Register(string password, string repeatedPassword, User user, string countryName)
        {
            var errors = new List<string>();
            if (password != repeatedPassword)
            {
                errors.Add("The password mismatched with the confirmation password");
            }

            var identityResult = await this.InsertElement(user, password);

            if (identityResult.Succeeded && errors.Count == 0)
            {
                await _signInManager.SignInAsync(user, false);
                try
                {
                    await this.CreateReleatedCountry(user, countryName);
                    return await Task.FromResult(new List<string> { this.Ok, this.GenerateJwtToken(user) });
                }
                catch (Exception e)
                {
                    await _signInManager.SignOutAsync();
                    await this.DeleteElement(user.Id);
                    errors.Add(e.Message);
                }
                
            }
            errors = errors.Concat(identityResult.Errors.Select(x => x.Description)).ToList();
            errors.Insert(0, this.Invalid);
            return await Task.FromResult(errors);
        }

        public override async Task<List<string>> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception e)
            {
                return new List<string> { this.Invalid, e.Message };
            }

            return  new List<string> { this.Ok, "Signed out" };
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

        public override string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Id)
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
