namespace EurekaMoviesBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieNowPlayingRepository : MongoGenericRepository<MovieNowPlaying>, IMovieNowPlayingRepository
    {
        public MovieNowPlayingRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
