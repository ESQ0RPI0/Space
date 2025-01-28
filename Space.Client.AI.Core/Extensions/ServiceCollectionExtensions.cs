using Microsoft.Extensions.DependencyInjection;
using Space.Client.AI.Core.Setup;

namespace Space.Client.AI.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupClientCopilot(this IServiceCollection services)
        {
            services.AddTransient<ICopilotAgent, FrontCopilotAgent>();

            return services;
        }

    }
}
