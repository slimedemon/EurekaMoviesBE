namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTrendingDayRepository : MongoGenericRepository<MovieTrendingDay>, IMovieTrendingDayRepository
    {
        public MovieTrendingDayRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
