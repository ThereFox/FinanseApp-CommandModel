using Application.Events.Realisation.Abstractions;
using CSharpFunctionalExtensions;
using Infrastructure.EvensSourcerer.EventProduce.Abstractions;
using Infrastructure.EvensSourcerer.EventProduce.MessageConverter;

namespace Infrastructure.EvensSourcerer.EventProduce;

public class KafkaEventProducer<T> : IEventProducer<T> 
    where T : IDBStateChangeEvent
{
    private readonly KafkaProducer _producer;
    private readonly IMessageConverter<string> _messageConverter;
    
    private readonly string _topicName;
    
    public KafkaEventProducer(string topicName, KafkaProducer producer)
    {
        _producer = producer;
        _topicName = topicName;
    }
    
    public async Task<Result> ProduceAsync(T data)
    {
        var serialisedEventResult = _messageConverter.Convert<T>(data);

        if (serialisedEventResult.IsFailure)
        {
            return serialisedEventResult;
        }
        
        return await _producer.SendDataToTopic(serialisedEventResult.Value, _topicName);
    }
}