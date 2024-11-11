using Application.Events.Realisation.Abstractions;

namespace Application.Events.Realisation;

public class BillCreatedEvent : IDBStateChangeEvent
{
    public Guid BillId { get; private set; }
    public Guid OwnerId { get; private set; }

    public BillCreatedEvent(Guid billId, Guid ownerId)
    {
        BillId = billId;
        OwnerId = ownerId;
    }
    
}