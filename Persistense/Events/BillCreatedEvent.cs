using Application.Events.Realisation.Abstractions;

namespace Application.Events.Realisation;

public class BillCreatedEvent : IDBStateChangeEvent
{
    public Guid BillId { get; set; }
    public Guid OwnerId { get; set; }
}