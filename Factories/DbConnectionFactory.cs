using Authentication_Service.Strategies;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Authentication_Service.Factories
{
    public class DbConnectionFactory :IDbConnectionFactory
    {
        private readonly IEnumerable<IDbConnectionStrategy> _strategies;
        public DbConnectionFactory(IEnumerable<IDbConnectionStrategy> strategies)
        {
            _strategies = strategies;
        }
        public SqlConnection GetConnection(string key)
        {
            var strategy = this._strategies.FirstOrDefault(s => s.Key == key);
            if (strategy is null)
                throw new InvalidOperationException($"No DB connection strategy found for key '{key}'.");


            return strategy.GetConnection();
        }
    }
}
