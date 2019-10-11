using backend.DAL.Repository.AbstractClasses;
using backend.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace backend.DAL.Repository
{
    public class UserRepository : AUserRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        private UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public override async Task DeleteElement(string elementId)
        {
            User user = await _userManager.FindByIdAsync(elementId);
            if (!(user is null))
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public override async Task<User> GetElementById(string elementId)
        {
            return await _userManager.FindByIdAsync(elementId);
        }

        public override async Task<List<User>> GetElementBySearch(string searchtext)
        {
            var users = _userManager.Users;

            var query = from user in users
                        where user.UserName.Contains(searchtext)
                        select user;

            return await Task.FromResult(query.ToList());
        }

        public override async Task<IEnumerable<User>> GetElements()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        public override async Task<IdentityResult> InsertElement(User element, string password)
        {
            return await _userManager.CreateAsync(element, password);
        }

        public async override Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public override Task UpdateElement(User element)
        {
            throw new NotImplementedException("Currently this function is unnecessary.");
        }

        public override async Task<User> FindByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
    }
}
