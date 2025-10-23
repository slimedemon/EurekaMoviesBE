namespace EurekaMovieBE.Data
{
    public class TmdbDbContext: DbContext
    {
        public TmdbDbContext(DbContextOptions<TmdbDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().ToCollection("movies");
            modelBuilder.Entity<MovieGenre>().ToCollection("movie_genres");
            modelBuilder.Entity<MovieNowPlaying>().ToCollection("movies_now_playing");
            modelBuilder.Entity<MoviePopular>().ToCollection("movies_popular");
            modelBuilder.Entity<MovieTopRated>().ToCollection("movies_top_rated");
            modelBuilder.Entity<MovieTrendingDay>().ToCollection("movies_trending_day");
            modelBuilder.Entity<MovieTrendingWeek>().ToCollection("movies_trending_week");
            modelBuilder.Entity<People>().ToCollection("peoples");
            modelBuilder.Entity<Similar>().ToCollection("similars");
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieNowPlaying> MovieNowPlayings { get; set; }
        public DbSet<MoviePopular> MoviePopulars { get; set; }
        public DbSet<MovieTopRated> MovieTopRateds { get; set; }
        public DbSet<MovieTrendingDay> MovieTrendingDays { get; set; }
        public DbSet<MovieTrendingWeek> MovieTrendingWeeks { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Similar> Similars { get; set; }
    }
}
