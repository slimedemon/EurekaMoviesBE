using EurekaMovieBE.Persistence.Repositories.Application.Implements;
using Microsoft.EntityFrameworkCore.Storage;

namespace EurekaMovieBE.Persistence.UnitOfWorks
{
    public class ApplicationUnitOfWork: IApplicationUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction = null;
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

        public ApplicationUnitOfWork(ApplicationDbContext context)
        { 
            _context= context;
            ApiResource = new ApiResourceRepository(_context);
            ApiResourceScope = new ApiResourceScopeRepository(_context);
            Client = new ClientRepository(_context);
            ClientGrantType = new ClientGrantTypeRepository(_context);
            ClientSecret = new ClientSecretRepository(_context);
            ClientScope = new ClientScopeRepository(_context);
            UserInfo = new UserInfoRepository(_context);
            User = new UserRepository(_context);
            Favorite = new FavoriteRepository(_context);
            WatchList = new WatchListRepository(_context);
            Rating = new RatingRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            ArgumentNullException.ThrowIfNull(_transaction, nameof(_transaction));
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            ArgumentNullException.ThrowIfNull(_transaction, nameof(_transaction));
            await _transaction.RollbackAsync();
        }
    }
}
