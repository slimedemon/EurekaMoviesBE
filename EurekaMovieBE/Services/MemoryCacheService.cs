using Microsoft.Extensions.Caching.Memory;

namespace EurekaMovieBE.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MemoryCacheService> _logger;
        private CancellationTokenSource _resetCacheToken = new();
        public MemoryCacheService(IMemoryCache memoryCache, ILogger<MemoryCacheService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public T? Get<T>(string key)
        {
            _logger.LogInformation($"{nameof(MemoryCacheService)}_Get: {key}");
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            _logger.LogInformation($"{nameof(MemoryCacheService)}_Set: {key}");
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }

        public void Remove(string key)
        {
            if (_resetCacheToken != null
                && !_resetCacheToken.IsCancellationRequested
                && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
