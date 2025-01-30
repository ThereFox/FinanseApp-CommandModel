using Domain.Interface;

namespace Domain.Events.Realisation;

public class BillCreatedEvent : DomainEvent
{
    public Guid OwnerId { get; private set; }

    public BillCreatedEvent(Guid billId, Guid ownerId, DateTime createTime) : base(billId, createTime)
    {
        OwnerId = ownerId;
    }
    
}