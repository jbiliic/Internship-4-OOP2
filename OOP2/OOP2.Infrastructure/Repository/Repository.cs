using Microsoft.EntityFrameworkCore;
using OOP2.Domain.Repository.Common;

namespace OOP2.Infrastructure.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context )
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task DeleteAsync(TEntity entity)
        {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
             
            return entity;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity);

            foreach (var prop in entry.Properties)
            {
                if (prop.Metadata.ClrType == typeof(DateTime) && prop.CurrentValue is DateTime dt)
                {
                    if (dt.Kind == DateTimeKind.Unspecified)
                        prop.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    if (dt.Kind == DateTimeKind.Local)
                        prop.CurrentValue = dt.ToUniversalTime();
                }
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
