using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Space.Forms.Registration;
using Space.Server.Services.NewSpace;
using Space.Server.Services.Registration;

namespace Space.Server.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceServices(this IServiceCollection services)
        {
            services.AddScoped<INewSpaceService, NewSpaceService>();

            return services;
        }

        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<UserRegistrationForm>, PasswordHasher<UserRegistrationForm>>();
            services.AddScoped<UserAuthorizationService>();

            return services;
        }
    }
}
