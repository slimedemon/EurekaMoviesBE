


using Duende.IdentityServer;
using EurekaMovieBE.Helpers;
using EurekaMovieBE.Services.DuendeServices;

namespace EurekaMovieBE.Extensions
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(DbSettingsOptions.OptionName).Get<DbSettingsOptions>();   
            var migrationsAssembly = typeof(Program).Assembly.FullName;

            services.AddIdentityServer()
                .AddSigningCredential(CryptographyHelper.CreateRsaKey(), IdentityServerConstants.RsaSigningAlgorithm.RS256)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(dbSettings?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(dbSettings?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddAspNetIdentity<User>()
                .AddProfileService<ProfileService>();

            return services;
        }
        

        public static IServiceCollection AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(DbSettingsOptions.OptionName).Get<DbSettingsOptions>();
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(databaseOptions?.PostgresConnection ?? throw new InvalidOperationException("PostgresConnection is not configured"));
                });

            services.AddDbContext<TmdbDbContext>(
                options =>
                {
                    options.UseMongoDB(databaseOptions?.MongoDbConnection ?? throw new InvalidOperationException("MongoDbConnection is not configured"),
                        databaseOptions?.MongoDbName ?? throw new InvalidOperationException("MongoDbName is not configured"));
                });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        { 
            return services;
        }

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<ITmdbUnitOfWork, TmdbUnitOfWork>();
            return services;
        }
    }
}
