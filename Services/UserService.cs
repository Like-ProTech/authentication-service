using Authentication_Service.Models;
using Authentication_Service.Records;
using Authentication_Service.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Authentication_Service.Services
{
    public class UserService :IUserService
    {
        private readonly IAuthRepository _repo;

        public UserService(IAuthRepository authRepository)
        {
            this._repo = authRepository;
        }
        public async Task<LoginResult> AuthenticateUser(string email, string password)
        {
            var user = await this._repo.GetUserByEmail(email);
            //User doeas not match any record in the database
            if (user is null)
                return new LoginResult(false, string.Empty);
            var hasher =new PasswordHasher<Users>();
            PasswordVerificationResult result= hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            //Password does not match
            if (result.Equals(PasswordVerificationResult.Failed))
                return new LoginResult(false, string.Empty);
            //All is good generate token and return :)
            return null;
        }
    }
}
