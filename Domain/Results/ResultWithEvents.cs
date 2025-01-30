using CSharpFunctionalExtensions;
using Domain.Interface;

namespace Domain.Results;

public record struct ResultWithEvent<T>(
    Result<T> Result,
    IReadOnlyList<DomainEvent> Events
);

public record struct ResultWithEvent
(
    Result Result,
    IReadOnlyList<DomainEvent> Events
);

public static class Converters
{
    
    public static ResultWithEvent AsFailureWithoutEvent(this Result result)
    {
        return new ResultWithEvent(result, []);
    }
    
    public static ResultWithEvent AsCommonFailureWithoutEvent<T>(this Result<T> result)
    {
        return new ResultWithEvent(result, []);
    }
    
    public static ResultWithEvent<T> AsFailureWithoutEvent<T>(this Result<T> result)
    {
        return new ResultWithEvent<T>(result, []);
    }
    
    public static ResultWithEvent AsCommonWithEvent<T>(this Result<T> result, List<DomainEvent> events)
    {
        return new ResultWithEvent(result, events);
    }
    
    public static ResultWithEvent WithEvent(this Result result, List<DomainEvent> events)
    {
        return new ResultWithEvent(result, events);
    }
    
    public static ResultWithEvent<T> WithEvent<T>(this Result<T> result, List<DomainEvent> events)
    {
        return new ResultWithEvent<T>(result, events);
    }
    
}
