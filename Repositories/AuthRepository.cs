using Authentication_Service.Factories;
using Authentication_Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Authentication_Service.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnectionFactory _factory;

        public AuthRepository(IDbConnectionFactory connectionFactory) {
            this._factory = connectionFactory;
        }

        public async Task<Users?> GetUserById(Guid Id)
        {
            try
            {
                using var conn = this._factory.GetConnection("primary");
                await conn.OpenAsync();
                using var cmd = new SqlCommand("SELECT Id, FirstName, LastName, Email, PasswordHash FROM Users WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Users
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Users> RegisterUser(Users user)
        {
            try
            {
                using var conn = this._factory.GetConnection("primary");
                conn.Open();
                using var cmd = new SqlCommand("INSERT INTO Users (Id,FirstName,LastName,Email, PasswordHash) VALUES (@Id,@FirstName , @LastName,@Email, @PasswordHash)", conn);
                var hasher = new PasswordHasher<Users>();
                user.PasswordHash=hasher.HashPassword(user, user.PasswordHash);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                
                if (await cmd.ExecuteNonQueryAsync() < 0)
                {
                    return null;
                }
                return user;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Users?> GetUserByEmail(string email)
        {
            using var conn = _factory.GetConnection("primary");
            await conn.OpenAsync();

            var query = "SELECT Id, Email, PasswordHash FROM Users WHERE Email = @Email";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Users
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                };
            }

            return null;
        }

    }
}
