using EurekaMovieBE.Data;
using EurekaMovieBE.Entities.Tmdb;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTopRatedRepository : MongoGenericRepository<MovieTopRated>, IMovieTopRatedRepository
    {
        public MovieTopRatedRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
