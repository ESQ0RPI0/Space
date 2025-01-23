using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;

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
