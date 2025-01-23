using Application.Events.Realisation.Abstractions;
using CSharpFunctionalExtensions;

namespace Infrastructure.EvensSourcerer.EventProduce.MessageConverter;

public interface IMessageConverter<TOut>
{
    public Result<TOut> Convert<TEvent>(TEvent message)
        where TEvent : IDBStateChangeEvent;
}