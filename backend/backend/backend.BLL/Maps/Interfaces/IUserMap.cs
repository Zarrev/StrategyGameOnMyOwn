using backend.BLL.Classes;
using backend.Model.Frontend;
using backend.Model.Frontend.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface IUserMap
    {
        string Invalid { get; }
        string Ok { get; }
        Task<UserResponseContainer> Login(LoginDto model);
        Task<UserResponseContainer> Register(RegisterDto model);
        Task<UserResponseContainer> LogOut();
        Task<List<UserView>> GetUsers();
        Task<List<UserView>> GetUsersBySearchWithPoints(string searchText);
    }
}
