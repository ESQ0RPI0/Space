using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> List([FromQuery] PagingForm form)
        {
            return View();
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
