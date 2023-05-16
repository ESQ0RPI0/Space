using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Space.Forms.Registration;
using Space.Server.Services.NewSpace;
using Space.Server.Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
