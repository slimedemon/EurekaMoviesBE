
using EurekaMovieBE.Persistence.Repositories.Tmdb.Implements;

namespace EurekaMovieBE.Persistence.UnitOfWorks
{
    public class TmdbUnitOfWork : ITmdbUnitOfWork
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
            MovieGenre = new MovieGenreRepository(context, CollectionNameConstants.MovieGenres);
            MovieNowPlaying = new MovieNowPlayingRepository(context, CollectionNameConstants.MoviesNowPlaying);
            Movie = new MovieRepository(context, CollectionNameConstants.Movies);
            MoviePopular = new MoviePopularRepository(context, CollectionNameConstants.MoviesPopular);
            MovieTopRated = new MovieTopRatedRepository(context, CollectionNameConstants.MoviesTopRated);
            MovieTrendingDay = new MovieTrendingDayRepository(context, CollectionNameConstants.MoviesTrendingDay);
            MovieTrendingWeek = new MovieTrendingWeekRepository(context, CollectionNameConstants.MoviesTrendingWeek);
            People = new PeopleRepository(context, CollectionNameConstants.People);
            Similar = new SimilarRepository(context, CollectionNameConstants.Similar);
        }
    }
}
