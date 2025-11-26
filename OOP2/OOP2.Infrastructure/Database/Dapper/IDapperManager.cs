namespace OOP2.Infrastructure.Database.Dapper
{
    public interface IDapperManager
    {
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);
        Task<IReadOnlyList<T>> QueryGetAllAsync<T>(string sql, object? param = null);

    }
}
