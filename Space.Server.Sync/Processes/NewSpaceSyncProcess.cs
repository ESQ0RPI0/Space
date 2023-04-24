using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.Server.Datamodel.Common.Settings;
using Space.Shared.Api.ApiResults;
using System.Net;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Processes
{
    public class NewSpaceSyncProcess
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _settings;
        private readonly IOptionsMonitor<NewSpacePageMarkings> _newSpacePageMarkings;

        public NewSpaceSyncProcess(ILogger logger,
            IOptionsMonitor<ConnectionStrings> settings,
            IOptionsMonitor<NewSpacePageMarkings> newSpacePageMarkings)
        {
            _logger = logger;
            _settings = settings;
            _newSpacePageMarkings = newSpacePageMarkings;
        }

        public async Task<ServerResult<bool>> Sync()
        {
            var url = _settings.CurrentValue.NewSpaceConnectionString;

            if (url == null || string.IsNullOrEmpty(url))
            {
                return ServerErrorCodes.ConnectionStringError.WithExtras<bool>("Connection string not found");
            }

            var web = new HtmlWeb();
            var doc = web.Load(url);

            if (web.StatusCode != HttpStatusCode.OK)
            {
                return ServerErrorCodes.WebResourceLoadError;
            }

            _logger.LogTrace("Page was loaded");

            var table = doc.GetElementbyId(_newSpacePageMarkings.CurrentValue.TableTargetId);

            if (table == null)
                return ServerErrorCodes.WebResourceLoadError.WithExtras<bool>("Table not founded");

            var tableBody = table.SelectSingleNode("//table");

            foreach (var row in tableBody.SelectNodes(".//tr"))
            {
                var columns = row.SelectNodes("//td");
            }

            return ServerResults.CachedTrue;
        }
    }
}
