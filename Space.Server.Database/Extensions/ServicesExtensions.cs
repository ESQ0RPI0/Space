using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Space.Server.Database.Context;

namespace Space.Server.Database.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddNewSpaceDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<NewSpaceContext>(options => options.UseSqlServer("Server=(localdb)/MSSQLLocalDB;Database=SpaceDb;Trusted_Connection=True;"));

            return services;
        }
    }
}
