using HtmlAgilityPack;

namespace Space.Server.Sync.Interfaces
{
    /// <summary>
    /// Generic one-layer wrapper. Current implementation should only includes direct context implementation. Base wrapper and sub-services will be added later
    /// </summary>
    /// <typeparam name="TRecord"></typeparam>
    public interface IGenericCacheWrapper<TRecord> where TRecord : class
    {
        Task<TRecord?> Get(string key, CancellationToken cancellationToken);
        Task<bool> AddOrUpdate(string key, TRecord item, CancellationToken cancellationToken);
        Task Remove(string key, CancellationToken cancellationToken);
        Task<bool> IsExist(string key, CancellationToken cancellationToken);
    }
}
