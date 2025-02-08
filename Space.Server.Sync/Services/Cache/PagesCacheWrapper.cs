using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Space.Server.Sync.Interfaces;
using System.Text.Json;

namespace Space.Server.Sync.Services.Cache
{
    internal sealed class PagesCacheWrapper : IGenericCacheWrapper<HtmlDocument>
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<PagesCacheWrapper> _logger;

        public PagesCacheWrapper(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            };

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<bool> AddOrUpdate(string key, HtmlDocument item, CancellationToken cancellationToken)
        {
            try
            {
                await _cache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(item, _serializerOptions), _options, cancellationToken);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Error occured during set operation of {nameof(PagesCacheWrapper)}.", exception: ex);
                return false;
            }
        }

        public async Task<HtmlDocument?> Get(string key, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cache.GetAsync(key, cancellationToken);

                if (result is not null)
                    return JsonSerializer.Deserialize<HtmlDocument>(result, _serializerOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Error occured during get operation of {nameof(PagesCacheWrapper)}.", exception: ex);
            }

            return null;
        }

        public async Task<bool> IsExist(string key, CancellationToken cancellationToken)
        {
            var record = await _cache.GetAsync(key, cancellationToken);

            return record is not null;
        }

        public async Task Remove(string key, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}
