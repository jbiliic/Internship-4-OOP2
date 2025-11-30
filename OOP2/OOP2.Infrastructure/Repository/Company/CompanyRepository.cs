using Microsoft.EntityFrameworkCore;
using OOP2.Domain.Repository.Company;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Database.Dapper;

namespace OOP2.Infrastructure.Repository.Company
{
    public class CompanyRepository : Repository<Domain.Entities.Company.Company, int>, ICompanyRepository
    {
        private readonly IDapperManager _manager;
        private readonly CompanyDbContext _context;
        private readonly HttpClient _client;

        public CompanyRepository(CompanyDbContext context, HttpClient client ,IDapperManager manager) : base(context, client)
        {
            _manager = manager;
            _context = context;
            _client = client;
        }

        public async Task<bool> CompanyNameExistsAsync(string name)
        {
            var sql = "SELECT COUNT(1) FROM companies WHERE name = @Name";
            using var conn = new Npgsql.NpgsqlConnection(_manager.ConnectionString);
            conn.Open();
            var count = await _manager.QueryFirstOrDefaultAsync<int>(sql, new {Name = name });
            return count > 0;
        }
        public async Task<Domain.Entities.Company.Company?> GetCompanyByIdAsync(int id)
        {
            var sql = "SELECT * FROM companies WHERE id = @Id";
            return await _manager.QueryFirstOrDefaultAsync<Domain.Entities.Company.Company>(sql, new { Id = id });
        }
        public async Task<IReadOnlyList<Domain.Entities.Company.Company>> GetAllCompaniesAsync()
        {
            var sql = "SELECT * FROM companies";
            return await _manager.QueryGetAllAsync<Domain.Entities.Company.Company>(sql);
        }
    }
}
