using backend.Model.Frontend.Account;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface IUserMap
    {
        Task<object> Login(LoginDto model);
        Task<object> Register(RegisterDto model);
    }
}
