namespace EurekaMovieBE.Services
{
    public interface IMemoryCacheService
    {
        void Set<T>(string key, T value);
        T? Get<T>(string key);
        void Remove(string key);
    }
}
