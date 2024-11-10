using Application.Events.Realisation.Abstractions;
using CSharpFunctionalExtensions;

namespace Infrastructure.EvensSourcerer.EventProduce.Abstractions;

public interface IEventProducer<TEvent>
    where TEvent : IDBStateChangeEvent
{
    public Task<Result> ProduceAsync(TEvent data);
}