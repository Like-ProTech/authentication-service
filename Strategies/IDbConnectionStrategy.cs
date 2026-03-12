using Microsoft.Data.SqlClient;
namespace Authentication_Service.Strategies
{
    public interface IDbConnectionStrategy
    {
        string Key { get; }
        SqlConnection GetConnection();
    }
}
