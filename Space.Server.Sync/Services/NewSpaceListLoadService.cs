using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Space.Server.Services.Common;
using Space.Server.Sync.Interfaces;
using Space.Server.Sync.Processes.NewSpace.SubProcesses.NewSpace;
using Space.Shared.Api.ApiResults;
using System.Net;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Services
{
    internal sealed class NewSpaceListLoadService : IPageLoadService
    {
        private readonly IGenericCacheWrapper<HtmlDocument> _cache;
        private readonly DateTimeService _dateTime;
        private readonly ILogger<NewSpaceListLoadService> _logger;

        public NewSpaceListLoadService(IGenericCacheWrapper<HtmlDocument> cache, DateTimeService dateTime, ILogger<NewSpaceListLoadService> logger)
        {
            _cache = cache;
            _dateTime = dateTime;
            _logger = logger;
        }

        public async Task<ServerResult<HtmlDocument?>> LoadAsync(string url, CancellationToken cancellationToken)
        {
            var key = $"{_dateTime.GetCurrentDateTime().DayOfYear}_{nameof(NewSpacePageLoadSubProcess)}";

            if (await _cache.IsExist(key, cancellationToken))
                return await _cache.Get(key, cancellationToken);

            return await LoadDocumentAsync(url, key, cancellationToken);
        }

        private async Task<ServerResult<HtmlDocument?>> LoadDocumentAsync(string url, string key, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"{nameof(NewSpaceListLoadService)}: Start of the loading operation for NewSpace page");

                var web = new HtmlWeb();

                _logger.LogInformation($"Page loading for NewSpace sync process started, date time {_dateTime.GetCurrentDateTime()}");

                var doc = await web.LoadFromWebAsync(url, cancellationToken);

                if (web.StatusCode != HttpStatusCode.OK)
                    return ServerErrorCodes.WebResourceLoadError;

                await _cache.AddOrUpdate(key, doc, cancellationToken);

                _logger.LogInformation($"Page for NewSpace was loaded, date time {_dateTime.GetCurrentDateTime()}");

                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{nameof(NewSpaceListLoadService)}: Error occured during page load part of load process", exception: ex);
                throw;
            }
            finally
            {
                _logger.LogInformation($"{nameof(NewSpaceListLoadService)}: End of the NewSpace page load operation");
            }
        }
    }
}
