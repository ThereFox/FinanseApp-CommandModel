using Domain.Interface;

namespace Domain.Events.Realisation;

public class ClientCreatedEvent : DomainEvent
{
    public ClientCreatedEvent(Guid clientId, DateTime happenDateTime, string clientName) : base(clientId, happenDateTime)
    {
        ClientName = clientName;
    }

    public string ClientName { get; init; }
}