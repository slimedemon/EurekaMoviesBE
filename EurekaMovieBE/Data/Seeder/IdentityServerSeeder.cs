using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;

namespace EurekaMovieBE.Data.Seeder
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
                    ClientId = "eurekamoviebe",
                    ClientName = "EurekaMovieBE",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret123456".Sha256()) },
                    AllowedScopes = { "EurekaMovieBEAPI" }
                };
                context.Clients.Add(client.ToEntity());
            }

            if (!context.ApiResources.Any())
            {
                var api = new ApiResource("EurekaMovieBEAPI", "EurekaMovieBEAPI");
                context.ApiResources.Add(api.ToEntity());
            }

            if (!context.ApiScopes.Any())
            {
                var apiScope = new ApiScope("EurekaMovieBEAPI", "EurekaMovieBEAPI");
                context.ApiScopes.Add(apiScope.ToEntity());
            }

            context.SaveChanges();
        }
    }
}
