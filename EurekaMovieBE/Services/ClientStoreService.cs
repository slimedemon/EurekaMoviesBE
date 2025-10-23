using Duende.IdentityServer.Stores;

namespace EurekaMovieBE.Services
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

        public Task<Duende.IdentityServer.Models.Client?> FindClientByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
