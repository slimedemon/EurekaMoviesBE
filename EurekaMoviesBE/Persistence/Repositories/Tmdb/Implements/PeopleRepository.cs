namespace EurekaMoviesBE.Persistence.Repositories.Tmdb.Implements
{
    public class PeopleRepository : MongoGenericRepository<People>, IPeopleRepository
    {
        public PeopleRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
