using MongoDB.Driver;
namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces
{
    public interface IMongoGenericRepository<T>
    {
        Task CreateAsync(T entity);
        IFindFluent<T, T> GetAll();
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
        IFindFluent<T, T> Where(Expression<Func<T, bool>> predicate);
        IFindFluent<T, T> Where(FilterDefinition<T> filter);
        Task<T> GetByIdAsync(long id);
    }
}
