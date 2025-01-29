using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Space.Server.AI.Core.Settings;
using Space.Server.AI.Logic.Interfaces;
using Space.Shared.Api.ApiResults;
using System.Text;
using System.Text.Json;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.AI.Logic.Services
{
    internal sealed class ServerAiAgent : IServerAiAgent
    {
        private readonly IOptionsMonitor<OllamaSettings> _settings;
        private readonly Kernel _kernel;
        private readonly ILogger<ServerAiAgent> _logger;

        public ServerAiAgent(IOptionsMonitor<OllamaSettings> settings, Kernel kernel, ILogger<ServerAiAgent> logger)
        {
            _settings = settings;
            _kernel = kernel;
            _logger = logger;
        }

        public async Task<ServerResult<TResult>?> ExecuteOperation<TResult>(string prompt, CancellationToken cancellationToken)
        {
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var promptSettings = new OpenAIPromptExecutionSettings
            {
                ResponseFormat = typeof(TResult),
                Temperature = _settings.CurrentValue.Temperature,
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            try
            {
                var history = new ChatHistory();
                history.AddSystemMessage($"Only use functions to get the data that needed to complete the request");
                history.AddUserMessage(prompt);

                var chatService = _kernel.GetRequiredService<IChatCompletionService>();

                var response = await chatService.GetChatMessageContentAsync(history, promptSettings, cancellationToken: cancellationToken, kernel: _kernel);

                var result = JsonSerializer.Deserialize<TResult>(response.ToString());

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Critical error during prompt request for {nameof(TResult)} data, message: {ex.Message}");
                return ServerErrorCodes.InvalidData.WithExtras<TResult>(message: ex.Message);
            }
        }

        public async Task<ServerResult<string>> ExecuteOperation(string prompt, CancellationToken cancellationToken)
        {
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var promptSettings = new OpenAIPromptExecutionSettings
            {
                Temperature = _settings.CurrentValue.Temperature,
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            try
            {
                var history = new ChatHistory();
                history.AddSystemMessage($"Only use functions to get the data that needed to complete the request");
                history.AddUserMessage(prompt);

                var chatService = _kernel.GetRequiredService<IChatCompletionService>();

                StringBuilder response = new StringBuilder();
                await foreach (var item in chatService.GetStreamingChatMessageContentsAsync(history, promptSettings, cancellationToken: cancellationToken, kernel: _kernel))
                {
                    response.Append(item.ToString());
                }

                return response.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Critical error during prompt request for data, message: {ex.Message}");
                return ServerErrorCodes.InvalidData.WithExtras<string>(message: ex.Message);
            }
        }


    }
}
