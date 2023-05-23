using Microsoft.Extensions.DependencyInjection;
using Space.Client.Services.Mud;

namespace Space.Client.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddScoped<MudSnackbarDecoratorService>();

            return services;
        }
    }
}
