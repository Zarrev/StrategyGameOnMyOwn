using backend.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.DAL.Repository.AbstractClasses
{
    public abstract class AUserRepository: IBaseRepository<User, string>
    {
        public abstract Task DeleteElement(string elementId);

        public abstract void Dispose();

        public abstract Task<User> GetElementById(string elementId);

        public abstract Task<IEnumerable<User>> GetElements();

        public Task InsertElement(User element)
        {
            throw new System.NotImplementedException("This method cannot be use in this case!" +
                "Reason: It has no sense, because the UserManager need an User object and its password.");
        }

        public abstract Task Save();

        public abstract Task UpdateElement(User element);

        public abstract Task<IdentityResult> InsertElement(User element, string password);
    }
}
