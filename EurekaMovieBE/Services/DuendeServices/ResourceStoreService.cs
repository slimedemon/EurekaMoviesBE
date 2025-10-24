using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;

namespace EurekaMovieBE.Services.DuendeServices
{
    public class ResourceStoreService : IResourceStore
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ResourceStoreService(IApplicationUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Duende.IdentityServer.Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var dbApiResources = await _unitOfWork.ApiResource
                .GetAll()
                .Where(apiResource => apiResourceNames.Contains(apiResource.Name))
                .Select(apiResource => new Duende.IdentityServer.Models.ApiResource
                {
                    Name = apiResource.Name,
                    DisplayName = apiResource.DisplayName,
                    Scopes = apiResource.ApiResourceScopes.Select(s => s.Scope).ToList()
                }).AsNoTracking().ToListAsync();
            
            return dbApiResources;
        }

        public async Task<IEnumerable<Duende.IdentityServer.Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var dbApiResources = await _unitOfWork.ApiResource
                .GetAll()
                .Include(apiResource => apiResource.ApiResourceScopes)
                .Where(apiResource => apiResource.ApiResourceScopes.Any(scope => scopeNames.Contains(scope.Scope)))
                .Select(apiResource => new Duende.IdentityServer.Models.ApiResource
                {
                    Name = apiResource.Name,
                    DisplayName = apiResource.DisplayName,
                    Scopes = apiResource.ApiResourceScopes.Select(s => s.Scope).ToList()
                }).AsNoTracking().ToListAsync();

            return dbApiResources;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var apiScopes = await _unitOfWork.ApiResourceScope
                .GetAll()
                .Where(apiScope => scopeNames.Contains(apiScope.Scope))
                .Select(apiScope => new ApiScope
                {
                    Name = apiScope.Scope,
                }).AsNoTracking().ToListAsync();

            return apiScopes;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult<IEnumerable<IdentityResource>>(new List<IdentityResource>() { });
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var apiResources = await _unitOfWork.ApiResource
                .GetAll()
                .Select(apiResource => new Duende.IdentityServer.Models.ApiResource
                {
                    Name = apiResource.Name,
                    DisplayName = apiResource.DisplayName,
                }).AsNoTracking().ToListAsync();

            var apiScopes = await _unitOfWork.ApiResourceScope
                .GetAll()
                .Select(apiScope => new ApiScope
                {
                    Name = apiScope.Scope,
                }).AsNoTracking().ToListAsync();

            return new Resources(new List<IdentityResource>(), apiResources, apiScopes);
        }
    }
}
