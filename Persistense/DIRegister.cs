using App.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense
{
    public static class DIRegister
    {
        public static IServiceCollection AddPersistense(
            this IServiceCollection services,
            string connectionString
            )
        {
            services.AddDbContext<ApplicationDBContext>(
                options => options.UseNpgsql(connectionString)
            );
            services.AddScoped<IBillStore, BillStore>();
            services.AddScoped<IClientStore, ClientStore>();

            return services;
        }

    }
}
