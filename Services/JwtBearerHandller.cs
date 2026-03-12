using Authentication_Service.Models;
using Authentication_Service.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
namespace Authentication_Service.Services
{
    public class JwtBearerHandller : IJwtBearerHandller
    {
        private readonly IOptions<JwtSettings> _settings;

        public JwtBearerHandller(IOptions<JwtSettings> options)
        {
            this._settings = options;
        }
        public string GenerateToken(Users user)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.SecretKey)), SecurityAlgorithms.HmacSha256Signature),
                Audience = _settings.Value.Audience,
                Issuer = _settings.Value.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(_settings.Value.ExpirationMinutes),
                Subject =new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email ,user.Email),
                }),
                IssuedAt = DateTime.UtcNow

            };
            SecurityToken token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
