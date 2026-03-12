using Authentication_Service.Models;
using System.Runtime.CompilerServices;

namespace Authentication_Service.Repositories
{
    public interface IAuthRepository
    {
        Task<Users> RegisterUser(Users user);
        Task<Users?> GetUserByEmail(string email);
        Task<Users?> GetUserById(Guid Id);
    }
}
