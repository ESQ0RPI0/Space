using Microsoft.AspNetCore.Mvc;
using Space.Client.Forms.Basic;

namespace Space.Server.Controllers
{
    public class LaunchVehiclesController : Controller
    {
        public LaunchVehiclesController()
        {
            
        }
        public IActionResult List([FromQuery]PagingForm form)
        {
            return View();
        }
    }
}
