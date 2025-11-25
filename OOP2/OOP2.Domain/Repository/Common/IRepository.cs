namespace OOP2.Domain.Repository.Common
{
    public interface IRepository<TEntity, Tvalue> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Tvalue id);
        
    }
}
