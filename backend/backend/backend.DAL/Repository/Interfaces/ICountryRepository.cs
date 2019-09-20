using backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.DAL.Repository.Interfaces
{
    public interface ICountryRepository: IBaseRepository<Country, string>
    {
    }
}
