using Space.Server.Sync.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Space.Server.Sync.Database.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddSyncDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SyncDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
