using backend.BLL.Maps.Interfaces;
using backend.BLL.Services.AbstractClasses;
using backend.BLL.Services.Interfaces;
using backend.Model.Backend;
using backend.Model.Frontend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.BLL.Maps
{
    public class BattleMap : IBattleMap
    {
        private readonly IBattleService _service;
        private readonly AUserService _userService;
        private readonly ICountryService _countryService;

        public BattleMap(IBattleService service, AUserService userService, ICountryService countryService)
        {
            _service = service;
            _userService = userService;
            _countryService = countryService;
        }
        public async Task Create(BattleView elementViewModel, string userId)
        {
            await _service.InsertElement(await ViewModelToDomain(elementViewModel, userId));
            await _countryService.GoInToTheBattle(new MercenaryRequest {
                LaserShark =elementViewModel.LaserShark, BattleSeahorse=elementViewModel.BattleSeahorse, AssaultSeaDog=elementViewModel.AssaultSeaDog },
                userId);
        }

        public async Task<List<BattleView>> GetAllByUserId(string userId)
        {
            return await DomainToViewModel(await this._service.GetElementsByUserId(userId));
        }

        public Task Delete(string elementViewModelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BattleView>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BattleView> GetElement(string elementViewModelId)
        {
            throw new NotImplementedException();
        }

        public Task Update(BattleView elementViewModel)
        {
            throw new NotImplementedException();
        }

        private async Task<Battle> ViewModelToDomain(BattleView elementViewModel, string userId)
        {
            if (elementViewModel is null)
            {
                throw new System.ArgumentNullException(nameof(elementViewModel));
            }


            return await InitBattle(elementViewModel, userId); ;
        }

        private BattleView DomainToViewModel(Battle elementModel)
        {
            if (elementModel is null)
            {
                throw new System.ArgumentNullException(nameof(elementModel));
            }

            var viewModel = InitBattleView(elementModel);

            return viewModel;
        }

        private async Task<List<BattleView>> DomainToViewModel(List<Battle> elementModels)
        {
            var viewModels = new List<BattleView>();

            foreach (var model in elementModels)
            {
                model.User = await _userService.GetElementById(model.UserId);
                model.Enemy = await _userService.GetElementById(model.EnemyId);
                viewModels.Add(InitBattleView(model));
            }

            return await Task.FromResult(viewModels);
        }

        private async Task<Battle> InitBattle(BattleView model, string userId)
        {
            var enemy = await this._userService.FindByName(model.EnemyName);
            var user = await this._userService.GetElementById(userId);
            var enemyCountry = await this._countryService.GetElementByUserId(enemy.Id);
            return new Battle {
                EnemyId = enemy.Id,
                Enemy = enemy,
                User = user,
                UserId = user.Id,
                AssaultSeaDog = model.AssaultSeaDog,
                BattleSeahorse = model.BattleSeahorse,
                LaserShark = model.LaserShark,
                EnemyAssaultSeaDog = enemyCountry.AssaultSeaDog,
                EnemyBattleSeahorse = enemyCountry.BattleSeahorse,
                EnemyLaserShark = enemyCountry.LaserShark
            };
        }

        private BattleView InitBattleView(Battle model)
        {
            return new BattleView {
                EnemyName = model.Enemy.UserName,
                LaserShark = model.LaserShark,
                AssaultSeaDog = model.AssaultSeaDog,
                BattleSeahorse = model.BattleSeahorse
            };
        }

        public Task Create(BattleView elementViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
