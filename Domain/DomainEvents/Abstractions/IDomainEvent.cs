namespace Domain.Interface;

public abstract class DomainEvent
{
    public Guid ChangedEntityId { get; init; }
    public DateTime HappenDateTime { get; init; }

    public DomainEvent(Guid changedEntityId, DateTime happenDateTime)
    {
        ChangedEntityId = changedEntityId;
        HappenDateTime = happenDateTime;
    }
}