namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class SimilarRepository : MongoGenericRepository<Similar>, ISimilarRepository
    {
        public SimilarRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
