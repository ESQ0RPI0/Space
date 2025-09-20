using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Server.Datamodel.Common.Settings;
using Space.Server.Services.Interfaces;
using Space.Server.Sync.Processes.NewSpace.Requests;
using Space.Shared.Api.ApiResults;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Sync.Processes.NewSpace.SubProcesses.NewSpace
{
    /// <summary>
    /// Handling of Data parsing process
    /// </summary>
    internal sealed class NewSpaceDataParsingSubProcess : IRequestHandler<NsParseDataRequest, ServerResult<bool>>
    {
        private readonly ILogger<NewSpaceSyncProcess> _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _settings;
        private readonly IOptionsMonitor<NewSpacePageMarkings> _newSpacePageMarkings;
        private readonly IMapper _mapper;
        private readonly INewSpaceService _newSpaceService;

        public NewSpaceDataParsingSubProcess(IOptionsMonitor<ConnectionStrings> settings, ILogger<NewSpaceSyncProcess> logger,
            IOptionsMonitor<NewSpacePageMarkings> newSpacePageMarkings, IMapper mapper, INewSpaceService newSpaceService)
        {
            _settings = settings;
            _logger = logger;
            _newSpacePageMarkings = newSpacePageMarkings;
            _mapper = mapper;
            _newSpaceService = newSpaceService;
        }

        public async Task<ServerResult<bool>> Handle(NsParseDataRequest request, CancellationToken cancellationToken)
        {
            if (request.Doc is null)
                return ServerErrorCodes.WebResourceLoadError.WithExtras<bool>($"Doc field for {nameof(NsParseDataRequest)} is null");

            var table = request.Doc.GetElementbyId(_newSpacePageMarkings.CurrentValue.TableTargetId);

            if (table == null)
                return ServerErrorCodes.WebResourceLoadError.WithExtras<bool>("Table not founded");

            var tableBody = table.SelectSingleNode("//table");

            await _newSpaceService.AddOrUpdateExternalListItems(GetRows(), cancellationToken);

            return ServerResults.CachedTrue;

            IEnumerable<NsExternalListItemModel> GetRows()
            {
                foreach (var row in tableBody.SelectNodes(".//tr"))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogInformation($"{nameof(NewSpaceDataParsingSubProcess)}: Cancellation was requested");
                        yield break;
                    }
                    if (row.ChildNodes is null || row.ChildNodes.Count == 0)
                        continue;

                    if (row.ChildNodes.Any(u => u.Name == "th"))
                        continue;

                    var columns = row.SelectNodes(".//td");

                    var mappedRow = _mapper.Map<NsExternalListItemModel>(columns);

                    yield return mappedRow;
                }

            }
        }
    }
}
