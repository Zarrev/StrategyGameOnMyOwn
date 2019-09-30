using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Services.Interfaces
{
    public interface IRoundService
    {
        int ServerRoundNumber { get; }
        Task<int> IncrementRound();
    }
}
