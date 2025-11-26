using Dapper;
using Npgsql;

namespace OOP2.Infrastructure.Database.Dapper
{
    public class DapperManager : IDapperManager
    {
        private readonly string _connectionString;

        public DapperManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<IReadOnlyList<T>> QueryGetAllAsync<T>(string sql, object? param = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql, param);
            return result.ToList(); 
        }
    }
}
