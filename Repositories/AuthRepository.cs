using Authentication_Service.Factories;
using Authentication_Service.Models;
using Microsoft.Data.SqlClient;

namespace Authentication_Service.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnectionFactory _factory;

        public AuthRepository(IDbConnectionFactory connectionFactory) {
            this._factory = connectionFactory;
        }

        public void RegisterUser(Users user)
        {
            try
            {
                using var conn = this._factory.GetConnection("primary");
                using var cmd = new SqlCommand("INSERT INTO Users (FirstName,LastName,Email, PasswordHash) VALUES (@FirstName , @LastName,@Email, @PasswordHash)", conn);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Users? GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
