namespace EurekaMoviesBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTrendingWeekRepository : MongoGenericRepository<MovieTrendingWeek>, IMovieTrendingWeekRepository
    {
        public MovieTrendingWeekRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
