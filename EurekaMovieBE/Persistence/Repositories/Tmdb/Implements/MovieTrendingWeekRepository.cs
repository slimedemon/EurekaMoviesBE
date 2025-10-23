namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTrendingWeekRepository : MongoGenericRepository<MovieTrendingWeek>, IMovieTrendingWeekRepository
    {
        public MovieTrendingWeekRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
