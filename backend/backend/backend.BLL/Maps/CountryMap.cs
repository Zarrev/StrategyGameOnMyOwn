using backend.BLL.Maps.Interfaces;
using backend.BLL.Services.Interfaces;
using backend.Model;
using backend.Model.Frontend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL.Maps
{
    public class CountryMap: ICountryMap
    {
        private readonly ICountryService _service;

        public CountryMap(ICountryService service)
        {
            _service = service;
        }

        public async Task<int> GetRank(string userId)
        {
            return await this._service.GetRank(userId);
        }

        public async Task<CountryView> GetElement(string elementViewModelId)
        {
            return await _service.GetElementById(elementViewModelId).ContinueWith(task => CountryMap.DomainToViewModel(task.Result));
        }

        public async Task<CountryView> GetElementByUser(string userId)
        {
            var result = await _service.GetElementByUserId(userId);
            return CountryMap.DomainToViewModel(result);
        }

        public async Task<List<CountryView>> GetAll()
        {
            return await _service.GetElements().ContinueWith(task => CountryMap.DomainToViewModel(task.Result));
        }

        public async Task Create(CountryView elementViewModel)
        {
            await _service.InsertElement(ViewModelToDomain(elementViewModel));
        }

        public async Task Delete(string elementViewModelId)
        {
            await _service.DeleteElement(elementViewModelId);
        }

        public async Task Update(CountryView elementViewModel)
        {
            await _service.UpdateElement(ViewModelToDomain(elementViewModel));
        }

        private static Country ViewModelToDomain(CountryView elementViewModel)
        {
            if (elementViewModel is null)
            {
                throw new System.ArgumentNullException(nameof(elementViewModel));
            }

            var model = CountryMap.InitCountry(elementViewModel);

            return model;
        }

        private static CountryView DomainToViewModel(Country elementModel)
        {
            if (elementModel is null)
            {
                throw new System.ArgumentNullException(nameof(elementModel));
            }

            var viewModel = CountryMap.InitCountryView(elementModel);

            return viewModel;
        }

        private static List<CountryView> DomainToViewModel(List<Country> elementModels)
        {
            var viewModels = new List<CountryView>();

            foreach (var model in elementModels)
            {
                viewModels.Add(CountryMap.InitCountryView(model));
            }

            return viewModels;
        }

        private static CountryView InitCountryView(Country model)
        {
            return new CountryView
            {
                Id = model.Id,
                UserId = model.UserId,
                CountryName = model.CountryName,
                DevelopingName = model.DevelopingName,
                Coral = model.Coral,
                BuildingName = model.BuildingName,
                Alchemy = model.Alchemy,
                AssaultSeaDog = model.AssaultSeaDog,
                BattleSeahorse = model.BattleSeahorse,
                CoralWall = model.CoralWall,
                FlowController = model.FlowController,
                Inhabitant = model.Inhabitant,
                LaserShark = model.LaserShark,
                MudTractor = model.MudTractor,
                Pearl = model.Pearl,
                Points = model.Points,
                ReefCastle = model.ReefCastle,
                DevRounds = model.DevRounds,
                BuildRounds = model.BuildRounds,
                Sludgeharvester = model.Sludgeharvester,
                SonarGun = model.SonarGun,
                UnderwaterMaterialArts = model.UnderwaterMaterialArts
            };
        }

        private static Country InitCountry(CountryView model)
        {
            return new Country
            {
                Id = model.Id,
                UserId = model.UserId,
                CountryName = model.CountryName,
                DevelopingName = model.DevelopingName,
                BuildingName = model.BuildingName,
                Alchemy = model.Alchemy,
                AssaultSeaDog = model.AssaultSeaDog,
                BattleSeahorse = model.BattleSeahorse,
                CoralWall = model.CoralWall,
                FlowController = model.FlowController,
                Inhabitant = model.Inhabitant,
                LaserShark = model.LaserShark,
                MudTractor = model.MudTractor,
                Pearl = model.Pearl,
                Coral = model.Coral,
                Points = model.Points,
                ReefCastle = model.ReefCastle,
                DevRounds = model.DevRounds,
                BuildRounds = model.BuildRounds,
                Sludgeharvester = model.Sludgeharvester,
                SonarGun = model.SonarGun,
                UnderwaterMaterialArts = model.UnderwaterMaterialArts
            };
        }

        public async Task<int> GetInhabitant(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.Inhabitant);
        }

        public async Task<int> GetPearlNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.Pearl);
        }

        public async Task<int> GetFlowControllerNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.FlowController);
        }

        public async Task<int> GetReefCastleNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.ReefCastle);
        }

        public async Task<int> GetAssaultSeaDogNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.AssaultSeaDog);
        }

        public async Task<int> GetBattleSeahorseNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.BattleSeahorse);
        }

        public async Task<int> GetLaserSharkNumber(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.LaserShark);
        }

        public async Task<int> GetPoints(string userId)
        {
            await _service.CalcPoints(userId);
            return await _service.GetElementByUserId(userId).ContinueWith(task => task.Result.Points);
        }

        public async Task<List<bool>> GetDevelopments(string userId)
        {
            return await _service.GetDevelopments(userId);
        }

        public async Task<int> GetAttackValue(string userId)
        {
            return await _service.GetAttackValue(userId);
        }

        public async Task<int> GetDefenseValue(string userId)
        {
            return await _service.GetDefenseValue(userId);
        }

        public async Task<CountryView> FireNextRound(string userId)
        {
            var country = await _service.FinishRound(userId);
            return await Task.FromResult(DomainToViewModel(country));
        }

        public async Task Build(string userId, int buildingType)
        {
            await _service.Build(userId, buildingType);
        }

        public async Task Develop(string userId, int developType)
        {
            await _service.Build(userId, developType);
        }
        
        public async Task HireMercenary(string userId, MercenaryRequest mercenaryList)
        {
            await _service.HireMercenary(userId, mercenaryList);
        }
    }
}
