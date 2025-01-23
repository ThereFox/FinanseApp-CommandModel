using CSharpFunctionalExtensions;
using Infrastructure.EvensSourcerer.EventProduce;
using Infrastructure.TransactionalOutbox.Dapper;
using Infrastructure.TransactionalOutbox.DTOs;

namespace Persistense.Dapper.TransactionalOutbox.PollingPublisher;

public class PollingPublisherUseCase
{
    private const int BatchMaxSize = 5;
    private readonly OutboxStore _outboxStore;
    private readonly KafkaProducer _kafkaProducer;
    
    public PollingPublisherUseCase(OutboxStore store, KafkaProducer producer)
    {
        _outboxStore = store;
        _kafkaProducer = producer;
    }

    public async Task<Result> HandleMessageBatch()
    {
        try
        {
            var messages = await _outboxStore.GetUnhandledEventsAsync(BatchMaxSize);

            if (messages.Count == 0)
            {
                return Result.Success();
            }
            
            foreach (var changeEventDto in messages)
            {
                var handleMessageResult = await handleMessage(changeEventDto);

                if (handleMessageResult.IsFailure)
                {
                    return handleMessageResult;
                }
            }
            return Result.Success();
            
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

    private async Task<Result> handleMessage(ChangeEventDTO eventDto)
    {
        var sendResult = await _kafkaProducer.SendDataToTopic(eventDto.Message, eventDto.TargetTopic);

        if (sendResult.IsFailure)
        {
            return sendResult;
        }

        var commitResult = await _outboxStore.CommitEventHandlerAsync(eventDto.Id);

        return commitResult;
    }
}