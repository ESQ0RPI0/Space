using Microsoft.Extensions.DependencyInjection;
using Space.Server.Sync.Interfaces;
using Space.Server.Sync.Services;
using System.Reflection;

namespace Space.Server.Sync.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceSync(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IPageLoadService, NewSpaceListLoadService>();

            return services;
        }
    }
}
