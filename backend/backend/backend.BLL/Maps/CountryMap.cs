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

        public async Task<CountryView> GetElement(string elementViewModelId)
        {
            return await _service.GetElementById(elementViewModelId).ContinueWith(task => CountryMap.DomainToViewModel(task.Result));
        }

        public async Task<CountryView> GetElementByUser(string userId)
        {
            return await _service.GetElementByUserId(userId).ContinueWith(task => CountryMap.DomainToViewModel(task.Result));
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
                Rounds = model.Rounds,
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
                Rounds = model.Rounds,
                Sludgeharvester = model.Sludgeharvester,
                SonarGun = model.SonarGun,
                UnderwaterMaterialArts = model.UnderwaterMaterialArts
            };
        }
    }

}
