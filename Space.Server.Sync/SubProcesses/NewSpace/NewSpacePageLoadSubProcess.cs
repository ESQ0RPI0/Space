using HtmlAgilityPack;
using static Space.Shared.Common.Server.ServerTypes;
using System.Net;
using System.Runtime;
using Microsoft.Extensions.Options;
using Space.Server.Datamodel.Common.Settings;
using Space.Shared.Api.ApiResults;
using Microsoft.Extensions.Logging;
using Space.Server.Sync.Interfaces;
using Space.Server.Services.Common;

namespace Space.Server.Sync.SubProcesses.NewSpace
{
    internal sealed class NewSpacePageLoadSubProcess
    {
        private readonly IOptionsMonitor<NewSpacePageMarkings> _settings;
        private readonly IOptionsMonitor<ConnectionStrings> _connectionSettings;
        private readonly ILogger<NewSpacePageLoadSubProcess> _logger;

        private readonly IGenericCacheWrapper<HtmlDocument> _cache;
        private readonly DateTimeService _dateTime;

        public NewSpacePageLoadSubProcess(IOptionsMonitor<NewSpacePageMarkings> settings,
            IOptionsMonitor<ConnectionStrings> connectionSettings,
            ILogger<NewSpacePageLoadSubProcess> logger, IGenericCacheWrapper<HtmlDocument> cache, DateTimeService dateTime)
        {
            _settings = settings;
            _connectionSettings = connectionSettings;
            _logger = logger;
            _cache = cache;
            _dateTime = dateTime;
        }

        public async Task<ServerResult<HtmlDocument?>> Process(CancellationToken cancellationToken)
        {
            var url = _connectionSettings.CurrentValue.NewSpaceConnectionString;

            if (url == null || string.IsNullOrEmpty(url))
                return ServerErrorCodes.ConnectionStringError
                    .WithExtras<HtmlDocument?>("Connection string not found", ServerMessageTypes.Critical);

            var key = $"{_dateTime.GetCurrentDateTime()}_{nameof(NewSpacePageLoadSubProcess)}";

            if(await _cache.IsExist(key, cancellationToken))
                return await _cache.Get(key, cancellationToken);

            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);//takes too long to get this

            if (web.StatusCode != HttpStatusCode.OK)
                return ServerErrorCodes.WebResourceLoadError;
            
            await _cache.AddOrUpdate(key, doc, cancellationToken);

            _logger.LogTrace("Page was loaded");

            return doc;
        }
    }
}
