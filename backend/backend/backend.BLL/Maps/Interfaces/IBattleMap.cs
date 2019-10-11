using backend.Model.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface IBattleMap: IBaseMap<BattleView>
    {
        Task<List<BattleView>> GetAllByUserId(string userId);
        Task Create(BattleView elementViewModel, string userId);
    }
}
