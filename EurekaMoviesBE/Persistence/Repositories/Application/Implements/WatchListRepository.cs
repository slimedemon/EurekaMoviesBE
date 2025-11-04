
namespace EurekaMoviesBE.Persistence.Repositories.Application.Implements
{
    public class WatchListRepository : GenericRepository<WatchList>, IWatchListRepository
    {
        public WatchListRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
