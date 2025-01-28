using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Space.Client.AI.Core.Settings;

namespace Space.Client.AI.Core.Setup
{
    public interface ICopilotAgent
    {
        Task<string> ExecutePrompt(string prompt);
    }

    internal sealed class FrontCopilotAgent : ICopilotAgent
    {
        private readonly IConfiguration _configuration;

        public FrontCopilotAgent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ExecutePrompt(string prompt)
        {
            var kernel = InitializeSemanticKernel();

            if (kernel is null)
                return null;

            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

            var executionSettings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                Temperature = 0
            };

            ChatHistory chatHistory = [];
            chatHistory.AddSystemMessage("If your answer contains a decimal number, always show 1 digit after the decimal point.");
            chatHistory.AddUserMessage(prompt);
            var response = await chatCompletionService.GetChatMessageContentAsync(chatHistory, executionSettings, kernel);

            return response.ToString();
        }

        private Kernel? InitializeSemanticKernel()
        {
            var settings = _configuration.GetSection(nameof(CopilotSettings));

            if (settings is null)
                return null;

            var settingsModel = settings.Get<CopilotSettings>();
            if (settingsModel is null)
                return null;

            var host = new Uri(settingsModel.Host);
            var model = settingsModel.ModelName;

            var builder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.AddOllamaChatCompletion(model, host);
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton(_configuration);

            var kernel = builder.Build();

            return kernel;
        }
    }
}
