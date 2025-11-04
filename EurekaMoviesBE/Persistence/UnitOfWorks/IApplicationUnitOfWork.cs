using Microsoft.EntityFrameworkCore.Storage;

namespace EurekaMoviesBE.Persistence.UnitOfWorks
{
    public interface IApplicationUnitOfWork
    {
        public IUserInfoRepository UserInfo { get; }
        public IUserRepository User { get; }
        public IFavoriteRepository Favorite { get; }
        public IWatchListRepository WatchList { get; }
        public IRatingRepository Rating { get; }
        Task CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
