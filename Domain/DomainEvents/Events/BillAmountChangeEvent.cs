using Domain.Interface;

namespace Domain.Events.Realisation;

public class BillAmountChanged : DomainEvent
{
    public decimal NewAmount { get; private set; }

    public BillAmountChanged(Guid billId, decimal newAmount, DateTime changeTime) : base(billId, changeTime)
    {
        NewAmount = newAmount;
    }
}