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

        private double CalcDefenseForBattle(Battle battle)
        {
            double attackValue = 0;
            attackValue += 2 * battle.EnemyAssaultSeaDog;
            attackValue += 6 * battle.EnemyBattleSeahorse;
            attackValue += 5 * battle.EnemyLaserShark;

            return attackValue;
        }

        private double CalcAttackForBattle(Battle battle)
        {
            double attackValue = 0;
            attackValue += 6 * battle.AssaultSeaDog;
            attackValue += 2 * battle.BattleSeahorse;
            attackValue += 5 * battle.LaserShark;

            Random randomm = new Random();
            var random = ((randomm.NextDouble() * 10) - 5) / 100;
            double moralEffect = random * attackValue;

            return attackValue + moralEffect;
        }
        public async Task<List<BattleResult>> Fight(string userId)
        {
            var results = new List<BattleResult>();
            var battles = await this.GetElementsByUserId(userId);
            foreach (var battle in battles)
            {
                var attackValue = CalcAttackForBattle(battle);
                var defenseValue = CalcDefenseForBattle(battle);
                if (defenseValue > attackValue)
                {
                    results.Add(new BattleResult { Battle = battle, WinnerId = battle.EnemyId, LoserId = battle.UserId });
                    await this.DeleteElement(battle.Id);
                } else if (defenseValue < attackValue)
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
            bool killed = false;

            for (int i = 1; i <= sumLoss; i++)
            {
                if (killer == 0 && !killed)
                {
                    if (army.AssaultSeaDog != 0)
                    {
                        army.AssaultSeaDog--;
                        killed = true;
                    }
                    killer = 1;
                }
                if (killer == 1 && !killed)
                {
                    if (army.BattleSeahorse != 0)
                    {
                        army.BattleSeahorse--;
                        killed = true;
                    }
                    killer = 2;
                }
                if( killer == 2 && !killed)
                {
                    if (army.LaserShark != 0)
                    {
                        army.LaserShark--;
                        killed = true;
                    }
                    killer = 0;
                }
                killed = false;
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
