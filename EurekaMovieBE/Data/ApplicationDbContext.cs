using EurekaMovieBE.Data.Configurations;
using EurekaMovieBE.Entities.Auth;
using EurekaMovieBE.Entities.User;
using EurekaMovieBE.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientGrantTypes> ClientGrantTypes { get; set; }
        public DbSet<ClientSecrets> ClientSecrets { get; set; }
        public DbSet<ClientScopes> ClientScopes { get; set; }
        public DbSet<ApiResources> ApiResources { get; set; }
        public DbSet<ApiResourceScopes> ApiResourceScopes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<WatchList> WatchList { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
