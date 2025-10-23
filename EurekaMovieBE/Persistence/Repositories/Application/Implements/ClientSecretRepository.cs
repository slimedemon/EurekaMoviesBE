
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ClientSecretRepository : GenericRepository<ClientSecret>, IClientSecretRepository
    {
        public ClientSecretRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
