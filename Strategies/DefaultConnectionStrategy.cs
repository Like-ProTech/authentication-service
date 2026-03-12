using Authentication_Service.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Authentication_Service.Strategies
{
    public class DefaultConnectionStrategy : IDbConnectionStrategy
    {
        public string Key => "primary";
        public string _connectionString;
        public DefaultConnectionStrategy(IOptions<DefaultConnectionOption> connectionOption)
        {
            this._connectionString= connectionOption.Value.DefaultConnection;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}
