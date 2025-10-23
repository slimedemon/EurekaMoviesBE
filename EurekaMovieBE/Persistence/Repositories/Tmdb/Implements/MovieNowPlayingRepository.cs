using EurekaMovieBE.Data;
using EurekaMovieBE.Entities.Tmdb;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieNowPlayingRepository : MongoGenericRepository<MovieNowPlaying>, IMovieNowPlayingRepository
    {
        public MovieNowPlayingRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
