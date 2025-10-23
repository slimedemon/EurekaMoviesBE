namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieNowPlayingRepository : MongoGenericRepository<MovieNowPlaying>, IMovieNowPlayingRepository
    {
        public MovieNowPlayingRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
