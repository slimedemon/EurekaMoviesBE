using Microsoft.EntityFrameworkCore.Storage;

namespace EurekaMovieBE.Persistence.UnitOfWorks
{
    public interface IApplicationUnitOfWork
    {
        public IApiResourceRepository ApiResource { get; }
        public IApiResourceScopeRepository ApiResourceScope { get; }
        public IClientRepository Client { get; }
        public IClientGrantTypeRepository ClientGrantType { get; }
        public IClientSecretRepository ClientSecret { get; }
        public IClientScopeRepository ClientScope { get; }
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
