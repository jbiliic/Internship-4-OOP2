using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OOP2.Domain.Services;
using OOP2.Infrastructure.Database.Dapper;

namespace OOP2.Infrastructure.Repository.User
{
    public class AuthService : IAuthService
    {
        private readonly IDapperManager _manager;
        public AuthService(IDapperManager manager)
        {
            _manager = manager;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var sql = "SELECT COUNT(1) FROM users WHERE username = @U AND password = @P;";
            using var conn = new NpgsqlConnection(_manager.ConnectionString);
            var count = await conn.ExecuteScalarAsync<int>(sql, new { U = username, P = password });
            return count > 0;
        }
    }
}
