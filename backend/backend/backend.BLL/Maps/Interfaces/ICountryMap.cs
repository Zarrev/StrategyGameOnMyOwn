using backend.Model.Frontend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface ICountryMap: IBaseMap<CountryView>
    {
        Task<CountryView> GetElementByUser(string userId);
        Task<int> GetInhabitant(string userId);
        Task<int> GetPearlNumber(string userId);
        Task<int> GetFlowControllerNumber(string userId);
        Task<int> GetReefCastleNumber(string userId);
        Task<int> GetAssaultSeaDogNumber(string userId);
        Task<int> GetBattleSeahorseNumber(string userId);
        Task<int> GetLaserSharkNumber(string userId);
        Task<int> GetPoints(string userId);
        Task<CountryView> FireNextRound(string userId);
        Task<int> GetDefenseValue(string userId);
        Task<int> GetAttackValue(string userId);
        Task<List<bool>> GetDevelopments(string userId);
        Task Develop(string userId, int developType);
        Task Build(string userId, int buildingType);
        Task HireMercenary(string userid, MercenaryRequest mercenaryList);
        Task<int> GetRank(string userId);
    }
}
