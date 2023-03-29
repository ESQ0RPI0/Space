using Microsoft.AspNetCore.Mvc;
using Space.Server.Services.SpaceFund;

namespace Space.Server.Controllers.ExternalResources
{
    [ApiController]
    [Route("[controller]")]
    public class SpaceFundController : Controller
    {
        private readonly SpaceFundService _spaceFundService;

        public SpaceFundController(SpaceFundService spaceFundService)
        {
            _spaceFundService = spaceFundService;
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
