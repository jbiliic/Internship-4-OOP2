using Microsoft.Extensions.Caching.Memory;
using OOP2.Domain.Entities.User;
using OOP2.Domain.Services.Cache;
using OOP2.Infrastructure.External;

namespace OOP2.Infrastructure.Cache
{
    public class UserCacheService : IUserCacheService
    {
        private readonly IMemoryCache _cache;
        

        public UserCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public User? Get(string key)
        {
            var hit = _cache.TryGetValue(key, out User user);
            Console.WriteLine(hit ? $"Cache HIT → {key}" : $"Cache MISS → {key}");
            return hit ? user : null;
        }
        public void Set(string key , User users)
        {
            var now = DateTime.UtcNow;
            var endOfDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, DateTimeKind.Utc);
            var duration = endOfDay - now;

            _cache.Set(key, users, duration);
            Console.WriteLine($"Cache saved user {key} until end of day");
        }
    }
}
