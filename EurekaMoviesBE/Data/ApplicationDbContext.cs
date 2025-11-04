namespace EurekaMoviesBE.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        private const string DefaultSchema = "movieschema";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "45bfea26-0391-48ce-95a2-41b1f16c2633",
                        Name = SystemRole.Viewer,
                        NormalizedName = SystemRole.Viewer.ToUpper()
                    },
                    new IdentityRole
                    {
                        Id = "5a088ddf-b132-4269-9377-1711b06a2bc1",
                        Name = SystemRole.Administrator,
                        NormalizedName = SystemRole.Administrator.ToUpper()
                    }
                );  
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<WatchList> WatchList { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
