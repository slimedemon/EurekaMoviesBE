namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class SimilarRepository : MongoGenericRepository<Similar>, ISimilarRepository
    {
        public SimilarRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
