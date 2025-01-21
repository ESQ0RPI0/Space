using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Space.Client.AI.Core.Settings;

namespace Space.Client.Logic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetupClient(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(CopilotSettings));

            services.Configure<CopilotSettings>(settings);

            return services;
        }
    }
}
