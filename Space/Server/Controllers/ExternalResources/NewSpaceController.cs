using Microsoft.AspNetCore.Mvc;
using Space.Backend.Datamodel.Models.NewSpace;
using Space.Client.Forms.Basic;
using Space.Server.Services.NewSpace;
using Space.Server.Sync.Processes;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Controllers.ExternalResources
{
    [ApiController]
    [Route("[controller]")]
    public class NewSpaceController : Controller
    {
        private readonly NewSpaceService _newSpaceService;
        private readonly NewSpaceSyncProcess _newSpaceProcess;

        public NewSpaceController(NewSpaceService service,
            NewSpaceSyncProcess newSpaceProcess)
        {
            _newSpaceService = service;
            _newSpaceProcess = newSpaceProcess;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ServerResult<List<NewSpaceExternalListItemModel>>> List([FromQuery] PagingForm form)
        {
            return await _newSpaceService.GetList(form);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ServerResult<bool>> RunSync()
        {
            await _newSpaceProcess.Sync();
            return ServerResults.CachedTrue;
        }
    }
}
