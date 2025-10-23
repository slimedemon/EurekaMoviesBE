namespace EurekaMovieBE.Data
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
            modelBuilder.ApplyConfiguration(new ApiResourceScopeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientGrantTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientScopeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientSecretConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<WatchList> WatchList { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
