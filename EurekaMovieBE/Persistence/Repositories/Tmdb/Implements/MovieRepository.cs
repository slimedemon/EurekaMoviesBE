namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieRepository : MongoGenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
