
namespace EurekaMoviesBE.Persistence.Repositories.Application.Implements
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
