using System.Linq.Expressions;

namespace EurekaMoviesBE.Persistence.Repositories.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        void RemoveWithCondition(Expression<Func<T, bool>> expression);
    }
}
