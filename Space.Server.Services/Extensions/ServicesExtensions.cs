using Microsoft.Extensions.DependencyInjection;
using Space.Server.Services.NewSpace;
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
    }
}
