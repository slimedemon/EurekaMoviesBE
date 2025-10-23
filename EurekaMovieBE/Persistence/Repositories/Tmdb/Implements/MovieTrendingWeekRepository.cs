using EurekaMovieBE.Data;
using EurekaMovieBE.Entities.Tmdb;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTrendingWeekRepository : MongoGenericRepository<MovieTrendingWeek>, IMovieTrendingWeekRepository
    {
        public MovieTrendingWeekRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
