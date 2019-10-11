using backend.Model;
using backend.Model.Frontend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL.Services.Interfaces
{
    public interface ICountryService: IBaseService<Country, string>
    {
        Task<Country> GetElementByUserId(string userId);
        Task<int> GetRank(string userId);
        Task CalcPoints(string userId);
        Task<List<bool>> GetDevelopments(string userId);
        Task<int> GetAttackValue(string userId);
        Task<int> GetDefenseValue(string userId);
        Task<Country> FinishRound(string userId);
        Task Build(string userId, int buildingType);
        Task Develop(string userId, int developType);
        Task<string> HireMercenary(string userId, int LaserShark, int AssaultSeaDog, int BattleSeahorse, bool retuners = false);

        Task<int> GetDevRound(string userId);
        Task<int> GetBuildRound(string userId);
        Task<int> GetBuildingName(string userId);
        Task<int> GetDevelopingName(string userId);
        Task GoInToTheBattle(MercenaryRequest army, string userId);
    }
}
