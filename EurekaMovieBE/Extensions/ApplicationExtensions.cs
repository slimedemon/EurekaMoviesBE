using EurekaMovieBE.Data;
using EurekaMovieBE.Options;
using Microsoft.EntityFrameworkCore;

namespace EurekaMovieBE.Extensions
{
    public static class ApplicationExtensions
    {
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
    }
}
