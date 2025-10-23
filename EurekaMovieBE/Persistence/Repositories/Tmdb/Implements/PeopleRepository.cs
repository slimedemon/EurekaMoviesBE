namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class PeopleRepository : MongoGenericRepository<People>, IPeopleRepository
    {
        public PeopleRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
