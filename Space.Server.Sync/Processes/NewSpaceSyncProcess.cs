using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.Server.Datamodel.Models.Settings;
using Space.Shared.Api.ApiResults;
using System.Net;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Processes
{
    public class NewSpaceSyncProcess
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<ExternalResourcesOptions> _settings;

        public NewSpaceSyncProcess(ILogger logger,
            IOptionsMonitor<ExternalResourcesOptions> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public async Task<ServerResult<bool>> Sync()
        {
            var url = _settings.CurrentValue.NewSpaceConnectionString;

            if (url == null || string.IsNullOrEmpty(url))
            {
                return ServerErrorCodes.ConnectionStringError.WithExtras<bool>("dsdsd");
            }

            var web = new HtmlWeb();
            var doc = web.Load(url);

            if (web.StatusCode != HttpStatusCode.OK)
            {
                return ServerErrorCodes.WebResourceLoadError;
            }

            return ServerResults.CachedTrue;
        }
    }
}
