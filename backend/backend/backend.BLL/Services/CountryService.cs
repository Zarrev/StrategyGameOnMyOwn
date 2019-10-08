using backend.BLL.Services.Interfaces;
using backend.DAL.Repository.Interfaces;
using backend.Model;
using backend.Model.Backend;
using backend.Model.Frontend;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL
{
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetRank(string userId)
        {
            return await _repository.getCurrentRank(userId);
        }

        public async Task<List<Country>> GetElements()
        {
            return await _repository.GetElements().ContinueWith(task => (List<Country>) task.Result);
        }

        public async Task<Country> GetElementById(string elementId)
        {
            return await _repository.GetElementById(elementId);
        }

        public async Task<Country> GetElementByUserId(string userId)
        {
            return await _repository.getElementByUserId(userId);
        }

        public async Task InsertElement(Country element)
        {
            await _repository.InsertElement(element);
            await _repository.Save();
        }

        public async Task DeleteElement(string elementId)
        {
            await _repository.DeleteElement(elementId);
            await _repository.Save();
        }

        public async Task UpdateElement(Country element)
        {
            await _repository.UpdateElement(element);
            await _repository.Save();
        }

        public async Task<List<bool>> GetDevelopments(string userId)
        {
            return await this.GetElementByUserId(userId).ContinueWith(task => GetDevelopmentsForCountry(task.Result));
        }

        public async Task CalcPoints(string userId)
        {
            Country country = await this.GetElementByUserId(userId);
            country.Points = GetActualPointsForCountry(country);
            await this.UpdateElement(country);
        }

        public async Task<int> GetAttackValue(string userId)
        {
            Country country = await this.GetElementByUserId(userId);
            int attackValue = 0;
            int index = 0;
            this.GetMercenaryForCountry(country).ForEach(x => {
                switch (index)
                {
                    case 0: attackValue += 6 * x; break;
                    case 1: attackValue += 2 * x; break;
                    case 2: attackValue += 5 * x; break;
                }
                ++index;
            });

            return await Task.FromResult(attackValue);
        }

        public async Task<int> GetDefenseValue(string userId)
        {
            Country country = await this.GetElementByUserId(userId);
            int defenseValue = 0;
            int index = 0;
            this.GetMercenaryForCountry(country).ForEach(x => {
                switch (index)
                {
                    case 0: defenseValue += 2 * x; break;
                    case 1: defenseValue += 6 * x; break;
                    case 2: defenseValue += 5 * x; break;
                }
                ++index;
            });

            return await Task.FromResult(defenseValue);
        }

        public async Task<int> GetDevRounds(string userId)
        {
            return await this.GetElementByUserId(userId).ContinueWith(task => task.Result.DevRounds);
        }

        public async Task<int> GetBuildRounds(string userId)
        {
            return await this.GetElementByUserId(userId).ContinueWith(task => task.Result.BuildRounds);
        }

        public async Task<Country> FinishRound(string userId)
        {
            Country country = await this.GetElementByUserId(userId);
            country.Pearl += ImposeTax(country);
            country.Coral += HarvestCorall(country);
            country.Pearl -= CalcMercenaryPay(country);
            country.Coral -= CalcMercenaryEatingCost(country);
            country = DevelopmentReadiness(country);
            country = BuildingRediness(country);
            //battle
            country.Points = GetActualPointsForCountry(country);

            await UpdateElement(country);

            return await Task.FromResult(country);
        }

        public async Task Build(string userId, int buildingType)
        {
            var country = await GetElementByUserId(userId);
            if (Enum.GetName(typeof(BuildingEnum), buildingType) != null && country.BuildingName == -1)
            {
                country.BuildingName = buildingType;
                await UpdateElement(country);
            } else
            {
                // send a warning message to the user: You are building now, please wait the end of work (__ round remains).
            }
        }

        public async Task Develop(string userId, int developType)
        {
            var country = await GetElementByUserId(userId);
            if (Enum.GetName(typeof(DevelopmentEnum), developType) != null && country.DevelopingName == -1)
            {
                country.DevelopingName = developType;
                await UpdateElement(country);
            }
            else
            {
                // send a warning message to the user: You are developing now, please wait the end of work (__ round remains).
            }
        }

        public async Task<int> GetDevRound(string userId)
        {
            return await GetElementByUserId(userId).ContinueWith(task => task.Result.DevRounds);
        }

        public async Task<int> GetBuildRound(string userId)
        {
            return await GetElementByUserId(userId).ContinueWith(task => task.Result.BuildRounds);
        }

        public async Task<string> HireMercenary(string userId, int LaserShark, int AssaultSeaDog, int BattleSeahorse)
        {
            var country = await this.GetElementByUserId(userId);
            if (CanHire(country, LaserShark, AssaultSeaDog, BattleSeahorse))
            {
                country.LaserShark += LaserShark;
                country.AssaultSeaDog += AssaultSeaDog;
                country.BattleSeahorse += BattleSeahorse;
                await UpdateElement(country);
                return await Task.FromResult("OK");
            }

            return await Task.FromResult("You cannot feed that much mercenaries.");
            
        }

        public async Task<int> GetBuildingName(string userId)
        {
            return await this.GetElementByUserId(userId).ContinueWith(task => task.Result.BuildingName);
        }

        public async Task<int> GetDevelopingName(string userId)
        {
            return await this.GetElementByUserId(userId).ContinueWith(task => task.Result.DevelopingName);
        }

        private bool CanHire(Country country, int LaserShark, int AssaultSeaDog, int BattleSeahorse)
        {
            return ((country.Coral - (CalcMercenaryEatingCost(country) + CalcNewMercenariesEatingCost(LaserShark, AssaultSeaDog, BattleSeahorse)) >= 0));
        }

        private int CalcNewMercenariesEatingCost(int LaserShark, int AssaultSeaDog, int BattleSeahorse)
        {
            return LaserShark * 2 + AssaultSeaDog + BattleSeahorse;
        }

        private List<bool> GetDevelopmentsForCountry(Country country)
        {
            return new List<bool> { country.MudTractor, country.Sludgeharvester,
                country.CoralWall, country.SonarGun, country.UnderwaterMaterialArts, country.Alchemy };
        }

        private List<int> GetBuildingForCountry(Country country)
        {
            return new List<int> { country.FlowController, country.ReefCastle };
        }

        private List<int> GetMercenaryForCountry(Country country)
        {
            return new List<int> { country.AssaultSeaDog, country.BattleSeahorse, country.LaserShark };
        }

        private int GetActualPointsForCountry(Country country)
        {
            var newPoints = country.Inhabitant;

            GetBuildingForCountry(country).ForEach(x => newPoints += x * 50);

            var mercenaryNumberList = GetMercenaryForCountry(country);
            mercenaryNumberList.ForEach(x => newPoints += x * 5);
            newPoints += mercenaryNumberList[2] * 5;

            GetDevelopmentsForCountry(country).ForEach(x => {
                if (x)
                {
                    newPoints += 100;
                }
            });

            return newPoints;
        }

        private int ImposeTax(Country country)
        {
            double tax = country.Alchemy ? 25 * 1.3 : 25;
            return (int) (tax * country.Inhabitant);
        }

        private int HarvestCorall(Country country)
        {
            double coral = country.FlowController * 200;
            double increase = 1;
            double increaser = 0.1;
            GetDevelopmentsForCountry(country).GetRange(0, 2).ForEach(x => {
                if (x)
                {
                    increase += increaser;
                }
                increaser += 0.15;
            });

            return (int)(coral * increase);
        }

        private int CalcMercenaryPay(Country country)
        {
            return CostOfMercenaries(country, 3);
        }

        private int CalcMercenaryEatingCost(Country country)
        {
            return CostOfMercenaries(country, 2);
        }

        private int CostOfMercenaries(Country country, int laserSharkDifference)
        {
            var cost = 0;
            var index = 0;
            GetMercenaryForCountry(country).ForEach(x => {
                if (index > 1)
                {
                    cost += x;
                }
                else
                {
                    cost += x * laserSharkDifference;
                }
                ++index;
            });

            return cost;
        }

        private Country DevelopmentReadiness(Country country)
        {   
            if(Enum.GetName(typeof(DevelopmentEnum), country.DevelopingName) != null)
            {
                if (country.DevRounds == 15)
                {
                    switch (country.DevelopingName)
                    {
                        case 0: country.MudTractor = true; break;
                        case 1: country.Sludgeharvester = true; break;
                        case 2: country.CoralWall = true; break;
                        case 3: country.SonarGun = true; break;
                        case 4: country.UnderwaterMaterialArts = true; break;
                        case 5: country.Alchemy = true; break;
                    }
                    country.DevelopingName = -1;
                    country.DevRounds = 0;
                } else
                {
                    country.DevRounds++;
                }
            }

            return country;
        }

        private Country BuildingRediness(Country country)
        {
            if (Enum.GetName(typeof(BuildingEnum), country.BuildingName) != null)
            {
                if(country.BuildRounds == 5)
                {
                    switch (country.BuildingName)
                    {
                        case 0: country.FlowController += 1; break;
                        case 1: country.ReefCastle += 1; break;
                    }
                    country.BuildingName = -1;
                    country.BuildRounds = 0;
                } else
                {
                    country.BuildRounds++;
                }
                
            }

            return country;
        }
    }
}
