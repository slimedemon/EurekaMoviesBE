using MongoDB.Driver;
namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces
{
    public interface IMongoGenericRepository<T> where T : TmdbBase
    {
        Task AddAsync(T entity);
        IQueryable<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(object id);
    }
}
