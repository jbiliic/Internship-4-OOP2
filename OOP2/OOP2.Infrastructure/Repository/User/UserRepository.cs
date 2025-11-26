using OOP2.Domain.Repository.User;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Repository.Common;
using Microsoft.EntityFrameworkCore;
using OOP2.Infrastructure.Database;
using OOP2.Infrastructure.Database.Dapper;
namespace OOP2.Infrastructure.Repository.User
{
    public sealed class UserRepository : Repository<OOP2.Domain.Entities.User.User, int>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDapperManager _manager; 
        public UserRepository(DbContext context, IDapperManager dapperManager) : base(context)
        {
            _context = (ApplicationDbContext)context;
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

        public Task<bool> EmailExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Domain.Entities.User.User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Domain.Entities.User.User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserNameExistsAsync(string userName)
        {
            throw new NotImplementedException();
        }

        Task<Domain.Entities.User.User> IRepository<Domain.Entities.User.User, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
