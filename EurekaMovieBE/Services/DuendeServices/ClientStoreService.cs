using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace EurekaMovieBE.Services.DuendeServices
{
    public class ClientStoreService: IClientStore
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly ILogger<ClientStoreService> _logger;

        public ClientStoreService(IApplicationUnitOfWork unitOfWork, ILogger<ClientStoreService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Duende.IdentityServer.Models.Client?> FindClientByIdAsync(string clientId)
        {
            try
            {
                var clients = await _unitOfWork.Client
               .GetAll()
               .Where(client => client.ClientId == clientId)
               .Include(client => client.ClientGrantTypes)
               .Include(client => client.ClientScopes)
               .Include(client => client.ClientSecrets)
               .Select(client => new
               {
                   client.ClientId,
                   GrantTypes = client.ClientGrantTypes.Select(gt => gt.GrantType).ToList(),
                   Scopes = client.ClientScopes.Select(cs => cs.Scope).ToList(),
                   Secrets = client.ClientSecrets.Select(cs => cs.Secret).ToList(),
               }).AsNoTracking().ToListAsync();

                if (!clients.Any())
                {
                    return new Duende.IdentityServer.Models.Client();
                }

                var clientData = clients.Select(c => new Duende.IdentityServer.Models.Client
                {
                    ClientId = c.ClientId,
                    AllowedGrantTypes = c?.GrantTypes ?? new List<string>(),
                    AllowedScopes = c?.Scopes ?? new List<string>(),
                    ClientSecrets = c?.Secrets.Select(s => new Secret(s.Sha256())).ToList() ?? new List<Secret>()
                }).FirstOrDefault();

                return clientData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding client by ID: {ClientId}", clientId);
                return new Duende.IdentityServer.Models.Client();
            }
        }

        Task<global::Duende.IdentityServer.Models.Client?> IClientStore.FindClientByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
