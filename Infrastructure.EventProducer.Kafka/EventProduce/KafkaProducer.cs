using Confluent.Kafka;
using CSharpFunctionalExtensions;

namespace Infrastructure.EvensSourcerer.EventProduce;

public sealed class KafkaProducer
{
    private readonly IProducer<Null, string> _messageProduser;


    public KafkaProducer(IProducer<Null, string> messageProduser)
    {
        _messageProduser = messageProduser;
    }


    public async Task<Result> SendDataToTopic(string data, string topicName)
    {
        try
        {
            var message = new Message<Null, string>()
            {
                Value = data,
            };

            var deliveryResult = await _messageProduser.ProduceAsync(topicName, message);

            if (deliveryResult.Status != PersistenceStatus.Persisted)
            {
                return Result.Failure("error");
            }
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}