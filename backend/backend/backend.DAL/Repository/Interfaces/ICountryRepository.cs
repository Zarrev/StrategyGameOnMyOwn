using backend.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.DAL.Repository.Interfaces
{
    public interface ICountryRepository: IBaseRepository<Country, string>
    {
        Task<Country> getElementByUserId(string userId);
    }
}
