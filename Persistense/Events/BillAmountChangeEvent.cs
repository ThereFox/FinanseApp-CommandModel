using Application.Events.Realisation.Abstractions;

namespace Application.Events.Realisation;

public class BillAmountChangeEvent : IDBStateChangeEvent
{
    public Guid BillId { get; private set; }
    public decimal NewAmount { get; private set; }

    public BillAmountChangeEvent(Guid billId, decimal newAmount)
    {
        BillId = billId;
        NewAmount = newAmount;
    }
}