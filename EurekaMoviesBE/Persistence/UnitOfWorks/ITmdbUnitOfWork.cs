namespace EurekaMoviesBE.Persistence.UnitOfWorks
{
    public interface ITmdbUnitOfWork
    {
        public IMovieGenreRepository MovieGenre { get; }
        public IMovieNowPlayingRepository MovieNowPlaying { get; }
        public IMovieRepository Movie { get; }
        public IMoviePopularRepository MoviePopular { get; }
        public IMovieTopRatedRepository MovieTopRated { get; }
        public IMovieTrendingDayRepository MovieTrendingDay { get; }
        public IMovieTrendingWeekRepository MovieTrendingWeek { get; }
        public IPeopleRepository People { get; }
        public ISimilarRepository Similar { get; }
    }
}
