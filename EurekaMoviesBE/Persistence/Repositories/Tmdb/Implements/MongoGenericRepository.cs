namespace EurekaMoviesBE.Persistence.Repositories.Tmdb.Implements
{
    public class MongoGenericRepository<T> : IMongoGenericRepository<T> where T : TmdbBase
    {
        private readonly TmdbDbContext _context;
        private readonly IMongoCollection<T> _collection;

        public MongoGenericRepository(TmdbDbContext context, string collectionName)
        {
            _context = context;
            _collection = _context.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public IFindFluent<T, T> GetAll()
        {
            return _collection.Find(_ => true);
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(e => e.TmdbId == entity.TmdbId, entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _collection.DeleteOneAsync(e => e.TmdbId == id);
        }

        public IFindFluent<T, T> Where(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate);
        }

        public IFindFluent<T, T> Where(FilterDefinition<T> filter)
        {
            return _collection.Find(filter);
        }

        public Task<T> GetByIdAsync(long id)
        {
            return _collection.Find(e => e.TmdbId == id).FirstOrDefaultAsync();
        }
    }
}
