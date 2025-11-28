using System.Net.Http.Json;
using System.Xml.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.Common;
using OOP2.Domain.Repository.User;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Database.Dapper;
using OOP2.Infrastructure.External;
namespace OOP2.Infrastructure.Repository.User
{
    public sealed class UserRepository : Repository<OOP2.Domain.Entities.User.User, int>, IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly IDapperManager _manager; 
        private readonly HttpClient _httpClient;
        public UserRepository(UserDbContext context, IDapperManager dapperManager , HttpClient httpClient) : base(context,httpClient)
        {
            _context = (UserDbContext)context;
            _manager = dapperManager;
            _httpClient = httpClient;
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

        public override async Task<Domain.Entities.User.User> GetByIdAsync(int id)
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
        FROM users
        WHERE id = @Id;
    ";
            return await _manager.QueryFirstOrDefaultAsync<Domain.Entities.User.User>(sql, new { Id = id });
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
        public async Task<List<Domain.Entities.User.User>?> GetExternalUsersAsync()
        {
            try
            {
                var externalUsers = await _httpClient.GetFromJsonAsync<List<JsonPlaceholderUser>>(
                    "https://jsonplaceholder.typicode.com/users");

                if (externalUsers == null || externalUsers.Count == 0)
                    return null;

                var users = new List<Domain.Entities.User.User>();

                foreach (var ext in externalUsers)
                {
                    var full = ext.name.Trim();
                    var parts = full.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var user = new Domain.Entities.User.User
                    {
                        FirstName = parts[0],
                        LastName = parts.Length > 1 ? parts[^1] : "",
                        UserName = ext.username,
                        Email = ext.email,
                        AdressStreet = ext.address.street,
                        AdressCity = ext.address.city,
                        CoordinateLat = float.TryParse(ext.address.geo.lat, out var lat) ? lat : 0,
                        CoordinateLng = float.TryParse(ext.address.geo.lng, out var lng) ? lng : 0,
                        Website = ext.website,
                        Password = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    users.Add(user);
                }
                return users;
            }
            catch
            {
                return null;
            }
        }
    }
}
