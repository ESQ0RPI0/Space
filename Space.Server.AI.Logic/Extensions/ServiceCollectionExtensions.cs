using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Space.Server.AI.Core.Settings;
using Space.Server.AI.Logic.Interfaces;
using Space.Server.AI.Logic.Plugins;
using Space.Server.AI.Logic.Services;

namespace Space.Server.AI.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupSemanticKernelLogic(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settingsSection = configuration.GetSection(nameof(OllamaSettings));
            if (settingsSection is null)
                throw new ArgumentNullException(nameof(settingsSection));

            services.Configure<OllamaSettings>(settingsSection);

            var settings = settingsSection.Get<OllamaSettings>();
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            services.AddOpenAIChatCompletion(modelId: settings.ModelName, new Uri(settings.Host),
                    apiKey: Environment.GetEnvironmentVariable("GITHUB_TOKEN"));

            services.AddTransient(serviceProvider =>
            {
                var kernel = new Kernel(serviceProvider);

                kernel.ImportPluginFromType<LaunchVehiclesPlugin>();

                return kernel;
            });

#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            services.AddScoped<IServerAiAgent, ServerAiAgent>();

            return services;
        }
    }
}
