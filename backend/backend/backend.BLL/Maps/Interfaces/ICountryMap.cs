using backend.Model.Frontend;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface ICountryMap: IBaseMap<CountryView>
    {
        Task<CountryView> GetElementByUser(string userId);
    }
}
