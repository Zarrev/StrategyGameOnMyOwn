using backend.Model;
using System.Threading.Tasks;

namespace backend.BLL.Services.Interfaces
{
    public interface ICountryService: IBaseService<Country, string>
    {
        Task<Country> GetElementByUserId(string userId);
    }
}
