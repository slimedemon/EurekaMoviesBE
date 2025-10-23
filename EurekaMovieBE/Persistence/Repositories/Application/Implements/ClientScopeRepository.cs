
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ClientScopeRepository : GenericRepository<ClientScope>, IClientScopeRepository
    {
        public ClientScopeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
