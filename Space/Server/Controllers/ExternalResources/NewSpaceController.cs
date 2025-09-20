using Microsoft.AspNetCore.Mvc;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Server.AI.Logic.Interfaces;
using Space.Server.Services.Interfaces;
using Space.Server.Sync.Processes.NewSpace;
using Space.Server.Sync.Processes.NewSpace.Requests;
using Space.Shared.Api.ApiResults;
using System.Text.Json;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Controllers.ExternalResources
{
    [ApiController]
    [Route("[controller]")]
    public class NewSpaceController : Controller
    {
        private readonly INewSpaceService _newSpaceService;
        private readonly INewSpaceQueryService _newSpaceQueryService;
        private readonly NewSpaceSyncProcess _newSpaceProcess;
        private readonly IServerAiAgent _agent;

        public NewSpaceController(INewSpaceService service,
            NewSpaceSyncProcess newSpaceProcess,
            IServerAiAgent agent,
            INewSpaceQueryService newSpaceQueryService)
        {
            _newSpaceService = service;
            _newSpaceProcess = newSpaceProcess;
            _agent = agent;
            _newSpaceQueryService = newSpaceQueryService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ServerResult<List<NsRawItemViewModel>>> RawList([FromQuery] PagingForm form, CancellationToken cancellationToken)
        {
            return await _newSpaceQueryService.GetRawViewModelByForm(form, cancellationToken);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> RawListByPromptAsync([FromQuery] string prompt, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _agent.ExecuteOperation<List<NsRawLaunchVehicleViewModel>>(prompt, cancellationToken);

                if (!result.IsCorrect)
                    return result.Information.Code;

                return result;
            }
            catch (Exception ex)
            {
                return ServerErrorCodes.InvalidData;
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ServerResult<bool>> RunSync(CancellationToken cancellationToken)
        {
            await _newSpaceProcess.Handle(new NsStartProcessRequest(), cancellationToken);
            return ServerResults.CachedTrue;
        }
    }
}
