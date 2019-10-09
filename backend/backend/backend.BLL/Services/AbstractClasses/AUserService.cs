using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.BLL.Services.Interfaces;
using backend.Model;
using Microsoft.AspNetCore.Identity;

namespace backend.BLL.Services.AbstractClasses
{
    public abstract class AUserService : IBaseService<User, string>
    {
        public string Invalid { get { return "INVALID"; } }
        public string Ok { get { return "OK"; } }
        public abstract Task DeleteElement(string elementId);
        public abstract Task<User> GetElementById(string elementId);
        public abstract Task<List<User>> GetElements();
        public abstract Task<IdentityResult> InsertElement(User element, string password);
        public Task InsertElement(User element)
        {
            throw new NotImplementedException("This method cannot be use in this case!" +
                "Reason: It has no sense, because the UserManager need an User object and its password.");
        }
        public abstract Task UpdateElement(User element);
        public abstract Task<List<string>> Login(string username, string password);
        public abstract Task<List<string>> Register(string password, string repeatedPassword, User user, string countryName);
        public abstract Task<List<string>> LogOut();
        public abstract string GenerateJwtToken(User user);

        public abstract Task<List<KeyValuePair<User, KeyValuePair<int, int>>>> GetUsersWithPoints();
        public abstract Task<List<KeyValuePair<User, KeyValuePair<int, int>>>> GetUsersBySearchWithPoints(string searchtext);
    }
}
