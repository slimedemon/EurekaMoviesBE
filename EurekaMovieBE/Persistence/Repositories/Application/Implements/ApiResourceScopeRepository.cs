namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class ApiResourceScopeRepository : GenericRepository<ApiResourceScope>, IApiResourceScopeRepository
    {
        public ApiResourceScopeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
