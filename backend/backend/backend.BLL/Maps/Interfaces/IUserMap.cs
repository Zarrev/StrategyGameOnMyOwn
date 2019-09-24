using backend.Model.Frontend.Account;
using System.Threading.Tasks;

namespace backend.BLL.Maps.Interfaces
{
    public interface IUserMap
    {
        string Invalid { get; }
        string Ok { get; }
        Task<string[]> Login(LoginDto model);
        Task<string[]> Register(RegisterDto model);
    }
}
