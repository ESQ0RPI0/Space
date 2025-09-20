using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Space.Forms.Registration;
using Space.Server.Services.Common;
using Space.Server.Services.Interfaces;
using Space.Server.Services.NewSpace;
using Space.Server.Services.Registration;
using StackExchange.Redis;

namespace Space.Server.Services.Extensions
{
    public static class ServicesCollectionExtensions
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

        public static IServiceCollection AddGeneralServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(TimeProvider.System)
                .AddScoped<DateTimeService>();

            services.AddStackExchangeRedisCache(options =>
            {
                var conString = config.GetConnectionString("RedisConnection");

                if(string.IsNullOrEmpty(conString))
                    throw new ArgumentNullException(nameof(conString));

                options.ConnectionMultiplexerFactory = async () =>
                    await ConnectionMultiplexer.ConnectAsync(conString);
            });
            
            return services;
        }
    }
}
