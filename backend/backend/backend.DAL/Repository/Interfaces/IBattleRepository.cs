using backend.Model.Backend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.DAL.Repository.Interfaces
{
    public interface IBattleRepository: IBaseRepository<Battle, string>
    {
        Task<List<Battle>> getElementsByUserId(string userId);
    }
}
