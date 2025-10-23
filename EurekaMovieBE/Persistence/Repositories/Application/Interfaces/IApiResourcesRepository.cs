using EurekaMovieBE.Entities.Auth;
using EurekaMovieBE.Persistence.Repositories.Tmdb.Interfaces;

namespace EurekaMovieBE.Persistence.Repositories.Application.Interfaces
{
    public interface IApiResourcesRepository: IMongoGenericRepository<ApiResources>
    {
    }
}
