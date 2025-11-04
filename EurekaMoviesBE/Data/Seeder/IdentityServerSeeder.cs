using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;

namespace EurekaMoviesBE.Data.Seeder
{
    public class IdentityServerSeeder
    {
        public static void Seed(ConfigurationDbContext context)
        {
            // Ensure database created
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                var client = new Client
                {
                    ClientId = "EurekaMoviesBE",
                    ClientName = "EurekaMoviesBE",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret123456".Sha256()) },
                    AllowedScopes = { "EurekaMoviesBEAPI" }
                };
                context.Clients.Add(client.ToEntity());
            }

            if (!context.ApiResources.Any())
            {
                var api = new ApiResource("EurekaMoviesBEAPI", "EurekaMoviesBEAPI");
                context.ApiResources.Add(api.ToEntity());
            }

            if (!context.ApiScopes.Any())
            {
                var apiScope = new ApiScope("EurekaMoviesBEAPI", "EurekaMoviesBEAPI");
                context.ApiScopes.Add(apiScope.ToEntity());
            }

            context.SaveChanges();
        }
    }
}
