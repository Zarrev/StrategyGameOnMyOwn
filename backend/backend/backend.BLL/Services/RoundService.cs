using backend.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Services
{
    public class RoundService : IRoundService
    {
        public int ServerRoundNumber { get; private set; }
        public RoundService()
        {
            this.ServerRoundNumber = 0;
        }

        public Task<int> IncrementRound()
        {
            return Task.FromResult(++this.ServerRoundNumber);
        }
    }
}
