using OOP2.Domain.Entities.User;

namespace OOP2.Domain.Services.Cache
{
    public interface IUserCacheService
    {
        User? Get(string key);
        void Set(string key,User user);
    }
}
