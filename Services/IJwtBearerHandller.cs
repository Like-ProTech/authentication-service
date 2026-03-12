using Authentication_Service.Models;

namespace Authentication_Service.Services
{
    public interface IJwtBearerHandller
    {
        string GenerateToken(Users user);
    }
}
