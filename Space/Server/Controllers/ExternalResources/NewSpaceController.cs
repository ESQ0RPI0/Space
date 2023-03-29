using Microsoft.AspNetCore.Mvc;
using Space.Client.Forms.Basic;
using Space.Server.Services.NewSpace;

namespace Space.Server.Controllers.ExternalResources
{
    [ApiController]
    [Route("[controller]")]
    public class NewSpaceController : Controller
    {
        private readonly NewSpaceService _newSpaceService;

        public NewSpaceController(NewSpaceService service)
        {
            _newSpaceService = service;
        }
        public IActionResult List([FromQuery]PagingForm form)
        {
            return View();
        }
    }
}
