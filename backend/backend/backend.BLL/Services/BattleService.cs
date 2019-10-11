using backend.BLL.Services.Interfaces;
using backend.DAL.Repository.Interfaces;
using backend.Model;
using backend.Model.Backend;
using backend.Model.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _repository;

        public BattleService(IBattleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Battle>> GetElements()
        {
            return await _repository.GetElements().ContinueWith(task => (List<Battle>)task.Result);
        }

        public async Task<Battle> GetElementById(string elementId)
        {
            return await _repository.GetElementById(elementId);
        }

        public async Task<List<Battle>> GetElementsByUserId(string userId)
        {
            return await _repository.getElementsByUserId(userId);
        }

        public async Task InsertElement(Battle element)
        {
            await _repository.InsertElement(element);
            await _repository.Save();
        }

        public async Task DeleteElement(string elementId)
        {
            await _repository.DeleteElement(elementId);
            await _repository.Save();
        }

        public async Task UpdateElement(Battle element)
        {
            await _repository.UpdateElement(element);
            await _repository.Save();
        }

        private double CalcAttackForBattle(Battle battle)
        {
            double attackValue = 0;
            attackValue += 6 * battle.AssaultSeaDog;
            attackValue += 2 * battle.BattleSeahorse;
            attackValue += 5 * battle.LaserShark;

            Random random = new Random();
            double moralEffect = (random.NextDouble()*10 - 5) * attackValue;

            return attackValue + moralEffect;
        }
        public async Task<List<BattleResult>> Fight(string userId, int defensValue)
        {
            var results = new List<BattleResult>();
            var battles = await this.GetElementsByUserId(userId);
            foreach (var battle in battles)
            {
                var attackValue = CalcAttackForBattle(battle);
                if (defensValue > attackValue)
                {
                    results.Add(new BattleResult { Battle = battle, WinnerId = battle.EnemyId, LoserId = battle.UserId });
                    await this.DeleteElement(battle.Id);
                } else if (defensValue < attackValue)
                {
                    results.Add(new BattleResult { Battle = battle, WinnerId = battle.UserId, LoserId = battle.EnemyId });
                    await this.DeleteElement(battle.Id);
                } else
                {
                    results.Add(new BattleResult { Battle = battle, WinnerId = null, LoserId = null });
                    await this.DeleteElement(battle.Id);
                }
            }

            return await Task.FromResult(results);
        }

        public MercenaryRequest GetLoss(MercenaryRequest army)
        {
            int sumLoss = (int) Math.Round((army.AssaultSeaDog + army.BattleSeahorse + army.LaserShark) * 0.1);
            int killer = 0;        
            for (int i = 1; i <= sumLoss; i++)
            {
                if (killer == 0)
                {
                    army.AssaultSeaDog--;
                    killer = 1;
                } else if (killer == 1)
                {
                    army.BattleSeahorse--;
                    killer = 2;
                } else
                {
                    army.LaserShark--;
                    killer = 0;
                }
            }

            return army;
        }

        public List<int> Capture(Country country)
        {
            var capturedTreasures = new List<int>(2);
            int pearls = country.Pearl / 2;
            int corals = country.Coral / 2;
            capturedTreasures.Add(pearls);
            capturedTreasures.Add(corals);

            return capturedTreasures;
        }

    }
}
