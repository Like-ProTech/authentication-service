using Authentication_Service.Models;

namespace Authentication_Service.Repositories
{
    public interface IAuthRepository
    {
        void RegisterUser(Users user);
        Users? GetUserByEmail(string email);
    }
}
