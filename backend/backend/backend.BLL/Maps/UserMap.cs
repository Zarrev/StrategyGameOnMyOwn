using System.Collections.Generic;
using System.Threading.Tasks;
using backend.BLL.Maps.Interfaces;
using backend.Model;
using backend.Model.Frontend.Account;
using backend.BLL.Classes;
using backend.BLL.Services.AbstractClasses;
using backend.Model.Frontend;

namespace backend.BLL.Maps
{
    public class UserMap : IUserMap
    {
        private readonly AUserService _userService;
        public string Invalid { get { return _userService.Invalid; } }
        public string Ok { get { return _userService.Ok; } }

        public UserMap(AUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponseContainer> Login(LoginDto model)
        {
            var result = await _userService.Login(model.Username, model.Password);

            return new UserResponseContainer { Result = new List<string> { result[1] }, Validity = result[0] };
        }

        public async Task<UserResponseContainer> Register(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Username
            };
            var result = await _userService.Register(model.Password, model.RepeatedPassword, user, model.CountryName);
            
            return new UserResponseContainer { Result = result.GetRange(1, result.Count-1), Validity = result[0] };
        }

        public async Task<UserResponseContainer> LogOut()
        {
            var result = await _userService.LogOut();
            return new UserResponseContainer { Result = result.GetRange(1, result.Count-1), Validity = result[0] };
            
        }

        public async Task<List<UserView>> GetUsers()
        {
            return this.DomainToViewModel(await this._userService.GetUsersWithPoints());
        }

        public async Task<List<UserView>> GetUsersBySearchWithPoints(string searchText)
        {
            return this.DomainToViewModel(await this._userService.GetUsersBySearchWithPoints(searchText));
        }

        private List<UserView> DomainToViewModel(List<KeyValuePair<User, KeyValuePair<int, int>>> elementModels)
        {
            var viewModels = new List<UserView>();

            foreach (var model in elementModels)
            {
                viewModels.Add(this.InitUserView(model));
            }

            return viewModels;
        }

        private UserView InitUserView(KeyValuePair<User, KeyValuePair<int, int>> userWithPoints)
        {
            return new UserView { Id = userWithPoints.Key.Id, Username = userWithPoints.Key.UserName, Point = userWithPoints.Value.Key, Rank = userWithPoints.Value.Value };
        }
    }
}
