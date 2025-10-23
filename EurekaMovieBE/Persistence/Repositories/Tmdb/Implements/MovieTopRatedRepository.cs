namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieTopRatedRepository : MongoGenericRepository<MovieTopRated>, IMovieTopRatedRepository
    {
        public MovieTopRatedRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
