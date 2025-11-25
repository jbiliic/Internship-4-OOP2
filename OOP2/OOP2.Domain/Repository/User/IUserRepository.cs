using OOP2.Domain.Repository.Common;

namespace OOP2.Domain.Repository.User
{
    public interface IUserRepository : IRepository<Entities.User.User, int>
    {
        Task<bool> ActivateAsync(int userId);
        Task<bool> DeactivateAsync(int userId);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserNameExistsAsync(string userName);
    }
}
