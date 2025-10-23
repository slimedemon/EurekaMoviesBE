
using EurekaMovieBE.Persistence.Repositories.Tmdb.Implements;

namespace EurekaMovieBE.Persistence.UnitOfWorks
{
    public class TmdbUnitOfWork : ITmdbUnitOfWork, IDisposable
    {
        private readonly TmdbDbContext _context;

        public IMovieGenreRepository MovieGenre { get; }

        public IMovieNowPlayingRepository MovieNowPlaying { get; }

        public IMovieRepository Movie { get; }

        public IMoviePopularRepository MoviePopular { get; }

        public IMovieTopRatedRepository MovieTopRated { get; }

        public IMovieTrendingDayRepository MovieTrendingDay { get; }

        public IMovieTrendingWeekRepository MovieTrendingWeek { get; }

        public IPeopleRepository People { get; }

        public ISimilarRepository Similar { get; }

        public TmdbUnitOfWork(TmdbDbContext context)
        {
            _context = context;
            MovieGenre = new MovieGenreRepository(context);
            MovieNowPlaying = new MovieNowPlayingRepository(context);
            Movie = new MovieRepository(context);
            MoviePopular = new MoviePopularRepository(context);
            MovieTopRated = new MovieTopRatedRepository(context);
            MovieTrendingDay = new MovieTrendingDayRepository(context);
            MovieTrendingWeek = new MovieTrendingWeekRepository(context);
            People = new PeopleRepository(context);
            Similar = new SimilarRepository(context);
        }
    }
}
