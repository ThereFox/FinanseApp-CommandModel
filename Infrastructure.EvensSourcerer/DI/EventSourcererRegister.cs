using Application.Events.Realisation.Abstractions;
using Infrastructure.EvensSourcerer.EventProduce;
using Infrastructure.EvensSourcerer.EventProduce.MessageConverter;
using Microsoft.Extensions.DependencyInjection;
using KafkaProducer = Infrastructure.EvensSourcerer.EventProduce.KafkaProducer;

namespace Infrastructure.EvensSourcerer.DI;

public static class EventSourcererRegister
{
    public static IServiceCollection AddEventSourcerer(this IServiceCollection services)
    {
        services.AddSingleton<KafkaProducer>();
        services.AddScoped<IMessageConverter<string>, JsonMessageConverter>();
        
        return services;
    }

    public static IServiceCollection AddEventProducer<TEvent>(
        this IServiceCollection services,
        string topicName) 
        where TEvent : IDBStateChangeEvent
    {
        services.AddTransient<KafkaEventProducer<TEvent>>(
            ex =>
            {
                var producer = ex.GetRequiredService<KafkaProducer>();
                return new EventProduce.KafkaEventProducer<TEvent>(topicName, producer);
            }
        );

        return services;
    }
}