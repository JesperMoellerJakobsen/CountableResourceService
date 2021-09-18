using System.Data;
using System.Threading.Tasks;
using Dapper;
using Domain.Model;

namespace Repositories.Repositories
{
    public class CounterRepository : ICounterRepository
    {
        private const string SqlIncrement = "UPDATE [counter] SET [value] = [value] +1 WHERE [version] = @version";
        private const string SqlDecrement = "UPDATE [counter] SET [value] = [value] -1 WHERE [version] = @version";
        private const string SqlSelect = "SELECT * FROM [counter]";

        private readonly IDbConnection _connection;
        public CounterRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<ICounter> GetCounter()
        {
            using (_connection)
            {
                return await _connection.QueryFirstAsync<Counter>(SqlSelect);
            }
        }

        public async Task<bool> TryIncrement(byte[] clientCounterVersion)
        {
            return await ExecuteQuery(SqlIncrement, clientCounterVersion);
        }

        public async Task<bool> TryDecrement(byte[] clientCounterVersion)
        {
            return await ExecuteQuery(SqlDecrement, clientCounterVersion);
        }

        private async Task<bool> ExecuteQuery(string sqlStatement, byte[] version)
        {
            using (_connection)
            {
                var rowsUpdated = await _connection.ExecuteAsync(sqlStatement, new { version });
                return rowsUpdated == 1;
            }
        }
    }
}
