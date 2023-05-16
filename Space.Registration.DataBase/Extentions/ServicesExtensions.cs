using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Space.Registration.DataBase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Registration.DataBase.Extentions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRegistrationContext(this IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<UsersContext>(options => options.UseNpgsql(ConnectionString));

            return services;
        }
    }
}
