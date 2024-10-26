using App.CommandHandlers;
using App.CommandHandlers.Realisations;
using App.Commands;
using App.Commands.Realisations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class DIRegister
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreditBillCommand>, CreditBillCommandHandler>();
            services.AddScoped<ICommandHandler<DebetBillCommand>, DebetBillCommandHandler>();

            services.AddScoped<ICommandHandler<CreateBillCommand>, CreateBillCommandHandler>();
            services.AddScoped<ICommandHandler<CreateClientCommand>, CreateClientCommandHandler>();

            return services;

        }
    }
}
