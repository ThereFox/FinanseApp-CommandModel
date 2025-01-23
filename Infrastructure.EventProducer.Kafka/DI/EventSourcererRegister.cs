using Application.Events.Realisation.Abstractions;
using Confluent.Kafka;
using Infrastructure.EvensSourcerer.EventProduce;
using Infrastructure.EvensSourcerer.EventProduce.Abstractions;
using Infrastructure.EvensSourcerer.EventProduce.MessageConverter;
using Microsoft.Extensions.DependencyInjection;
using KafkaProducer = Infrastructure.EvensSourcerer.EventProduce.KafkaProducer;

namespace Infrastructure.EvensSourcerer.DI;

public static class EventSourcererRegister
{
    public static IServiceCollection AddEventProducer(this IServiceCollection services)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092",
            AllowAutoCreateTopics = true
        };
        
        var producer = new ProducerBuilder<Null, string>(config)
            .Build();
        
        services.AddSingleton<IProducer<Null, string>>(producer);
        services.AddSingleton<KafkaProducer>();
        services.AddScoped<IMessageConverter<string>, JsonMessageConverter>();
        
        return services;
    }

    public static IServiceCollection AddNotApplicableEventProducer<TEvent>(
        this IServiceCollection services,
        string topicName) 
        where TEvent : IDBStateChangeEvent
    {
        services.AddTransient<IEventProducer<TEvent>, KafkaEventProducer<TEvent>>(
            ex =>
            {
                var producer = ex.GetRequiredService<KafkaProducer>();
                var messageConverter = ex.GetRequiredService<IMessageConverter<string>>();
                return new EventProduce.KafkaEventProducer<TEvent>(topicName, producer, messageConverter);
            }
        );

        return services;
    }
}