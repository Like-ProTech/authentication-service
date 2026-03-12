namespace Authentication_Service.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string CurrentPassword);
        Boolean VerifyPassword(string CurrentPassword);
    }
}
