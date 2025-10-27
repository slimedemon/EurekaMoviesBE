namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieRepository : MongoGenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(TmdbDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}
