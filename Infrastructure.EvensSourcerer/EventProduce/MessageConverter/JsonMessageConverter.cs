using Application.Events.Realisation.Abstractions;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace Infrastructure.EvensSourcerer.EventProduce.MessageConverter;

public class JsonMessageConverter : IMessageConverter<string>
{
    public Result<string> Convert<TEvent>(TEvent message) where TEvent : IDBStateChangeEvent
    {
        return Result.Success(JsonConvert.SerializeObject(message));
    }
}