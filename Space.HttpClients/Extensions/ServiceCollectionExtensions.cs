using Microsoft.Extensions.DependencyInjection;
using Space.HttpClients;

namespace Space.FrontHttpClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services)
        {
            services.AddScoped<FrontApiClient>();

            return services;
        }
    }
}
