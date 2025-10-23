
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
