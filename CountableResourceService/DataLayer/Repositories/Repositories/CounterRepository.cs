using System.Data.SqlClient;
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

        private readonly ConnectionStrings _connectionString;
        public CounterRepository(ConnectionStrings connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<ICounter> GetCounter()
        {
            await using var connection = new SqlConnection(_connectionString.ConnectionString);
            await connection.OpenAsync();
            return await connection.QueryFirstAsync<Counter>(SqlSelect);
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
            await using var connection = new SqlConnection(_connectionString.ConnectionString);
            await connection.OpenAsync();
            var rowsUpdated = await connection.ExecuteAsync(sqlStatement, new { version });
            return rowsUpdated == 1;
        }
    }
}
