using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Space.Server.AI.Core.Settings;
using Space.Server.AI.Logic.Plugins;
using Space.Server.AI.Logic.Services;

namespace Space.Server.AI.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupSemanticKernelLogic(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settingsSection = configuration.GetSection(nameof(OllamaSettings));

            services.Configure<OllamaSettings>(settingsSection);

            var settings = settingsSection.Get<OllamaSettings>();

            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            services.AddOllamaChatCompletion(settings.ModelName, new Uri(settings.Host));

            services.AddTransient((serviceProvider) =>
            {
                var kernel = new Kernel(serviceProvider);
                kernel.Plugins.AddFromType<LaunchVehiclesPlugin>();

                return kernel;
            });

#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            services.AddScoped<ServerAiChatService>();

            return services;
        }
    }
}
