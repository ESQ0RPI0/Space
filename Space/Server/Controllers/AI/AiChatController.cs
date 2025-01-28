using Microsoft.AspNetCore.Mvc;
using Space.Server.AI.Logic.Interfaces;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Controllers.AI
{
    /// <summary>
    /// Basic chat operations for AI chat
    /// </summary>
    public class AiChatController : Controller
    {
        private readonly IServerAiAgent _chat;

        public AiChatController(IServerAiAgent chat)
        {
            _chat = chat;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ServerResult<string>> ExecutePromptAsync([FromBody] string promt, CancellationToken cancellationToken)
        {
            return await _chat.ExecuteOperation<string>(promt, cancellationToken);
        }
    }
}
