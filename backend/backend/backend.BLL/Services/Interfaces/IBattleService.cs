using backend.Model;
using backend.Model.Backend;
using backend.Model.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Services.Interfaces
{
    public interface IBattleService: IBaseService<Battle, string>
    {
        Task<List<Battle>> GetElementsByUserId(string userId);
        Task<List<BattleResult>> Fight(string userId, int defensValue);
        MercenaryRequest GetLoss(MercenaryRequest army);
        List<int> Capture(Country country);
    }
}
