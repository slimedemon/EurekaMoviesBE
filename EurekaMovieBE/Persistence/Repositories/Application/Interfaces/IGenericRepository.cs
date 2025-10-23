using System.Linq.Expressions;

namespace EurekaMovieBE.Persistence.Repositories.User.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(object id);
        Task<bool> Add(T entity);
        Task<bool> AddRange(List<T> entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool DeleteRange(List<T> entities);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task RemoveRangeAsync(Expression<Func<T, bool>> expression);
    }
}
