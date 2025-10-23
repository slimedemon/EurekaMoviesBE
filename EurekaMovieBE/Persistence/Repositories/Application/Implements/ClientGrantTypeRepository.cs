
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ClientGrantTypeRepository : GenericRepository<ClientGrantType>, IClientGrantTypeRepository
    {
        public ClientGrantTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
