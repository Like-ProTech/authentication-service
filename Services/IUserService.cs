using Authentication_Service.Records;

namespace Authentication_Service.Services
{
    public interface IUserService
    {
        public Task<LoginResult> AuthenticateUser(string email, string password);
    }
}
