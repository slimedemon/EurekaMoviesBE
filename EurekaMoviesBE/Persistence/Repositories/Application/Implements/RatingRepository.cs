
namespace EurekaMoviesBE.Persistence.Repositories.Application.Implements
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
