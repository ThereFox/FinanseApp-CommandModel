using App.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Stub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense
{
    public static class DIRegister
    {
        public static IServiceCollection AddPersistense(this IServiceCollection services)
        {
            services.AddScoped<IBillStore, BillStoreStub>();

            return services;
        }

    }
}
