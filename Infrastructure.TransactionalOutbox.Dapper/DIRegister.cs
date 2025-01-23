using Application.Events.Realisation.Abstractions;
using Infrastructure.EvensSourcerer.EventProduce.Abstractions;
using Infrastructure.TransactionalOutbox.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistense;

namespace Infrastructure.TransactionalOutbox;

public static class DIRegister
{
    public static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        services.AddScoped<OutboxStore>(
            ex =>
            {
                var context = ex.GetRequiredService<ApplicationDBContext>();
                return new OutboxStore(context.Database.GetDbConnection());
            }
            );
        
        return services;
    }

    public static IServiceCollection AddEventSender<TEvent>(this IServiceCollection services, string topic)
        where TEvent: IDBStateChangeEvent
    {
        services.AddScoped<IEventProducer<TEvent>, OutboxEventProducer<TEvent>>(
            ex =>
            {
                var store = ex.GetRequiredService<OutboxStore>();
                var currentTransaction = ex.GetRequiredService<ApplicationDBContext>().Database.CurrentTransaction?.GetDbTransaction();
                return new OutboxEventProducer<TEvent>(store, topic, currentTransaction);
            }
        );
        
        
        
        return services;
    }
}