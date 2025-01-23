using Microsoft.Extensions.DependencyInjection;
using Space.Client.Launchers;
using Space.HttpClients;

namespace Space.Client.Http.Extensions
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
