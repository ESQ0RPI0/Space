using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Datamodel.Common.Settings;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Server.Services.NewSpace;
using Space.Shared.Api.ApiResults;
using System.Net;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Processes
{
    public class NewSpaceSyncProcess
    {
        private readonly ILogger<NewSpaceSyncProcess> _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _settings;
        private readonly IOptionsMonitor<NewSpacePageMarkings> _newSpacePageMarkings;
        private readonly IMapper _mapper;
        private readonly NewSpaceService _newSpaceService;

        public NewSpaceSyncProcess(ILogger<NewSpaceSyncProcess> logger,
            IOptionsMonitor<ConnectionStrings> settings,
            IOptionsMonitor<NewSpacePageMarkings> newSpacePageMarkings,
            IMapper mapper,
            NewSpaceService newSpaceService)
        {
            _logger = logger;
            _settings = settings;
            _newSpacePageMarkings = newSpacePageMarkings;
            _mapper = mapper;
            _newSpaceService = newSpaceService;
        }

        public async Task<ServerResult<bool>> Sync() //partiate to subprocesses
        {
            var url = _settings.CurrentValue.NewSpaceConnectionString;

            if (url == null || string.IsNullOrEmpty(url))
            {
                return ServerErrorCodes.ConnectionStringError.WithExtras<bool>("Connection string not found", ServerMessageTypes.Critical);
            }

            var web = new HtmlWeb();
            var doc = web.Load(url);//takes too long to get this

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
                if (row.ChildNodes.Any(u => u.Name == "th"))
                    continue;

                var columns = row.SelectNodes(".//td");

                var mappedRow = _mapper.Map<NewSpaceExternalListItemModel>(columns);
                await _newSpaceService.AddExternalListItem(mappedRow);
            }

            return ServerResults.CachedTrue;
        }
    }
}
