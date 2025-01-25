using System.Data;
using System.Data.Common;
using Application.Events.Realisation.Abstractions;
using CSharpFunctionalExtensions;
using Infrastructure.EvensSourcerer.EventProduce.Abstractions;
using Infrastructure.TransactionalOutbox.Dapper;
using Infrastructure.TransactionalOutbox.DTOs;
using Newtonsoft.Json;

namespace Infrastructure.TransactionalOutbox;

public class OutboxEventProducer<TEvent> : IEventProducer<TEvent>
    where TEvent: IDBStateChangeEvent
{
    private readonly OutboxStore _store;
    private readonly string _topicName;
    private readonly TransactionGetter _currentTransactionGetter;
    
    
    public OutboxEventProducer(OutboxStore store, string topicName, TransactionGetter currentTransactionGetter)
    {
        _store = store;
        _topicName = topicName;
        _currentTransactionGetter = currentTransactionGetter;
    }
    
    public async Task<Result> ProduceAsync(TEvent data)
    {
        var eventData = new ChangeEventDTO()
        {
            Message = JsonConvert.SerializeObject(data),
            CreateTime = DateTime.UtcNow
        };

        var currentTransaction = _currentTransactionGetter.Transaction;

        if (currentTransaction == null)
        {
            return Result.Failure("event produced without transaction");
        }
        
        var saveResult = await _store.SaveEventAsync(eventData, _topicName, currentTransaction);

        return saveResult;
    }
}