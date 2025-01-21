using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Space.Server.AI.Core.Settings;

namespace Space.Server.AI.Logic.Services
{
    internal sealed class ServerAiChatService
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly IOptionsMonitor<OllamaSettings> _settings;
        private readonly Kernel _kernel;

        public ServerAiChatService(IChatCompletionService chatCompletionService, IOptionsMonitor<OllamaSettings> settings, Kernel kernel)
        {
            _chatCompletionService = chatCompletionService;
            _settings = settings;
            _kernel = kernel;
        }

        public async Task<string> ExecuteOperation(string prompt, CancellationToken cancellationToken)
        {
            var promptSettings = new OpenAIPromptExecutionSettings
            {
                Temperature = _settings.CurrentValue.Temperature,
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            ChatHistory chatHistory = [];
            chatHistory.AddSystemMessage("If your answer contains a decimal number, always show 1 digit after the decimal point.");
            chatHistory.AddUserMessage(prompt);
            var response = await _chatCompletionService.GetChatMessageContentAsync(chatHistory, promptSettings, _kernel);

            return response.ToString();
        }
    }
}
