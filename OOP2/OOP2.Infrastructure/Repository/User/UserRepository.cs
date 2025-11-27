using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.Common;
using OOP2.Domain.Repository.User;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Database.Dapper;
namespace OOP2.Infrastructure.Repository.User
{
    public sealed class UserRepository : Repository<OOP2.Domain.Entities.User.User, int>, IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly IDapperManager _manager; 
        public UserRepository(UserDbContext context, IDapperManager dapperManager) : base(context)
        {
            _context = (UserDbContext)context;
            _manager = dapperManager;
        }

        public Task<bool> ActivateAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var sql = "SELECT COUNT(1) FROM users WHERE email = @Email;";

            using var conn = new NpgsqlConnection(_manager.ConnectionString);
            await conn.OpenAsync();

            var count = await conn.ExecuteScalarAsync<int>(sql, new { Email = email });

            return count > 0;

        }

        public async Task<bool> UserNameExistsAsync(string username)
        {
            var sql = "SELECT COUNT(1) FROM users WHERE username = @UserName;";

            using var conn = new NpgsqlConnection(_manager.ConnectionString);
            await conn.OpenAsync();

            var count = await conn.ExecuteScalarAsync<int>(sql, new { UserName = username });

            return count > 0;
        }

        Task<Domain.Entities.User.User> IRepository<Domain.Entities.User.User, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Domain.Entities.User.User>> GetAllUsersAsync()
        {
            var sql = @"
        SELECT 
            id,
            username AS ""UserName"",
            name AS ""FirstName"",
            last_name AS ""LastName"",
            email AS ""Email"",
            website AS ""Website"",
            address_city AS ""AdressCity"",
            address_street AS ""AdressStreet"",
            birth_date AS ""BirthDate"",
            geo_lng AS ""CoordinateLng"",
            geo_lat AS ""CoordinateLat"",
            is_active AS ""IsActive"",
            created_at AS ""CreatedAt"",
            updated_at AS ""UpdatedAt"",
            password AS ""Password""
        FROM users;
    ";
            return await _manager.QueryGetAllAsync<Domain.Entities.User.User>(sql);
        }
    }
}
