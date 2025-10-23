
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ApiResourceRepository : GenericRepository<ApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
