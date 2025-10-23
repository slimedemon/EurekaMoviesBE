
namespace EurekaMovieBE.Persistence.Repositories.Application.Implements
{
    public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
