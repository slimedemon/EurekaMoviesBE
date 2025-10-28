namespace EurekaMovieBE.Data
{
    public class TmdbDbContext: DbContext
    {
        private readonly IMongoDatabase _database;

        public TmdbDbContext(DbContextOptions<TmdbDbContext> options, IConfiguration configuration) : base(options)
        {
            var databaseOptions = configuration.GetSection(DbSettingsOptions.OptionName).Get<DbSettingsOptions>();
            var mongoClient = new MongoClient(databaseOptions?.MongoDbConnection ?? throw new InvalidOperationException("MongoDbConnection is not configured"));
            var database = mongoClient.GetDatabase(databaseOptions?.MongoDbName ?? throw new InvalidOperationException("MongoDbName is not configured"));

            _database = database;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
