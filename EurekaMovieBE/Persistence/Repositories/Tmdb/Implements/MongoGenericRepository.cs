

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MongoGenericRepository<T> : IMongoGenericRepository<T> where T : TmdbBase
    {
        private readonly TmdbDbContext _context;
        private readonly DbSet<T> _dbSet;

        public MongoGenericRepository(TmdbDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
