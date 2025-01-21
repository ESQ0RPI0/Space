using Microsoft.AspNetCore.Mvc;

namespace Space.Server.Controllers.AI
{
    public class AiChatController : Controller
    {
        private readonly IChatCompletionService _chat;

        public AiChatController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
