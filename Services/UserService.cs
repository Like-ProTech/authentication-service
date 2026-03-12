using Authentication_Service.Models;
using Authentication_Service.Records;
using Authentication_Service.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Authentication_Service.Services
{
    public class UserService :IUserService
    {
        private readonly IAuthRepository _repo;
        private readonly IJwtBearerHandller _jwtHandller;

        public UserService(IAuthRepository authRepository,IJwtBearerHandller jwtBearerHandller)
        {
            this._repo = authRepository;
            this._jwtHandller = jwtBearerHandller;
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
            string token = this._jwtHandller.GenerateToken(user);
            return new LoginResult(true,token);
        }
    }
}
