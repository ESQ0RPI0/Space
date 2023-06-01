using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Space.Server.Database.Context;

namespace Space.Server.Database.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NewSpaceContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
