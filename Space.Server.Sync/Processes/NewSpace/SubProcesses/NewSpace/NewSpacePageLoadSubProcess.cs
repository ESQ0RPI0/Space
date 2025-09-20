using HtmlAgilityPack;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.Server.Datamodel.Common.Settings;
using Space.Server.Sync.Interfaces;
using Space.Server.Sync.Processes.NewSpace.Requests;
using Space.Shared.Api.ApiResults;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Processes.NewSpace.SubProcesses.NewSpace
{
    internal sealed class NewSpacePageLoadSubProcess : IRequestHandler<NsPageLoadRequest, ServerResult<HtmlDocument?>>
    {
        private readonly IOptionsMonitor<NewSpacePageMarkings> _settings;
        private readonly IOptionsMonitor<ConnectionStrings> _connectionSettings;
        private readonly ILogger<NewSpacePageLoadSubProcess> _logger;
        private readonly IPageLoadService _pageLoadService;


        public NewSpacePageLoadSubProcess(IOptionsMonitor<NewSpacePageMarkings> settings,
            IOptionsMonitor<ConnectionStrings> connectionSettings,
            ILogger<NewSpacePageLoadSubProcess> logger,
            IPageLoadService pageLoadService)
        {
            _settings = settings;
            _connectionSettings = connectionSettings;
            _logger = logger;
            _pageLoadService = pageLoadService;
        }

        public async Task<ServerResult<HtmlDocument?>> Handle(NsPageLoadRequest request, CancellationToken cancellationToken)
        {
            var url = _connectionSettings.CurrentValue.NewSpaceConnectionString;

            if (url == null || string.IsNullOrEmpty(url))
                return ServerErrorCodes.ConnectionStringError
                    .WithExtras<HtmlDocument?>("Connection string not found", ServerMessageTypes.Critical);

            var result = await _pageLoadService.LoadAsync(url, cancellationToken);

            if (!result.IsCorrect)
                _logger.LogError($"{nameof(NewSpacePageLoadSubProcess)}: error occured during page load phase");

            return result;
        }
    }
}
