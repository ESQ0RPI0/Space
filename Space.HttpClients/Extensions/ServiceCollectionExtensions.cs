using Microsoft.Extensions.DependencyInjection;
using Space.FrontHttpClient.Launchers;
using Space.HttpClients;

namespace Space.FrontHttpClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddScoped<FrontApiClient>();
            services.AddScoped<LaunchVehiclesApiClient>();

            return services;
        }
    }
}
