using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Space.Forms.Registration;
using Space.Server.Services.NewSpace;
using Space.Server.Services.Registration;
using Space.Server.Services.Sync;

namespace Space.Server.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceServices(this IServiceCollection services)
        {
            services.AddScoped<NewSpaceService>();

            return services;
        }

        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<UserRegistrationForm>, PasswordHasher<UserRegistrationForm>>();
            services.AddScoped<UserAuthorizationService>();

            return services;
        }

        public static IServiceCollection AddSyncServices(this IServiceCollection services)
        {
            services.AddScoped<SyncOperationService>();
            services.AddScoped<SyncValidationService>();

            return services;
        }
    }
}
