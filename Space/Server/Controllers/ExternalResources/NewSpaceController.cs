using Microsoft.AspNetCore.Mvc;
using Space.Client.Forms.Basic;
using Space.Server.Services.NewSpace;
using Space.Server.Sync.Processes;
using Space.Shared.Api.Types;

namespace Space.Server.Controllers.ExternalResources
{
    [ApiController]
    [Route("[controller]")]
    public class NewSpaceController : Controller
    {
        private readonly NewSpaceService _newSpaceService;
        private readonly NewSpaceSyncProcess _newSpaceProcess;

        public NewSpaceController(NewSpaceService service, NewSpaceSyncProcess newSpaceProcess)
        {
            _newSpaceService = service;
            _newSpaceProcess = newSpaceProcess;
        }
        public IActionResult List([FromQuery]PagingForm form)
        {
            return View();
        }

        [HttpGet]
        public async Task<ServerResult<bool>> RunsSync()
        {
            await _newSpaceProcess.Sync();
            return ServerResult<bool>.CachedTrue;
        }
    }
}
