using Space.Server.Datamodel.Common.Settings;

namespace Space.Server.Extensions
{
    public static class SettingsExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NewSpacePageMarkings>(configuration.GetSection(nameof(NewSpacePageMarkings)));
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

            return services;
        }
    }
}
