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
        public async Task DeleteAsync(TId id)
        {
            var enitiy = await _dbSet.FindAsync(id);
            if (enitiy != null)
            {
                _dbSet.Remove(enitiy);
            }
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

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
