using Microsoft.Extensions.DependencyInjection;
using Space.Server.Sync.Processes;

namespace Space.Server.Sync.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceSync(this IServiceCollection services)
        {
            services.AddScoped<NewSpaceSyncProcess>();

            return services;
        }
    }
}
