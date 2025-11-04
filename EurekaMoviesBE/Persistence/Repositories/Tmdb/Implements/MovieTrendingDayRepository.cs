namespace EurekaMoviesBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTrendingDayRepository : MongoGenericRepository<MovieTrendingDay>, IMovieTrendingDayRepository
    {
        public MovieTrendingDayRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
