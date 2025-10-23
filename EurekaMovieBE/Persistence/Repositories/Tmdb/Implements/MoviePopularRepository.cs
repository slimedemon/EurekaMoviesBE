using EurekaMovieBE.Data;
using EurekaMovieBE.Entities.Tmdb;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MoviePopularRepository : MongoGenericRepository<MoviePopular>, IMoviePopularRepository
    {
        public MoviePopularRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
