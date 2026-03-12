using Microsoft.Data.SqlClient;

namespace Authentication_Service.Factories
{
    public interface IDbConnectionFactory
    {
        SqlConnection GetConnection(string key);
    }
}
