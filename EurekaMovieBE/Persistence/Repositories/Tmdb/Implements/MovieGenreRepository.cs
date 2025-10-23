namespace EurekaMovieBE.Persistence.Repositories.Tmdb.Implements
{
    public class MovieGenreRepository : MongoGenericRepository<MovieGenre>, IMovieGenreRepository
    {
        public MovieGenreRepository(TmdbDbContext context) : base(context)
        {
        }
    }
}
