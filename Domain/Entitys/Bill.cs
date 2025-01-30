using CSharpFunctionalExtensions;
using Domain.Events.Realisation;
using Domain.Interface;
using Domain.Operation;
using Domain.Results;

namespace Domain.Entitys
{
    public sealed class Bill : Entity<Guid>
    {
        private readonly List<BillChanges> _changes;

        public IReadOnlyList<BillChanges> Changes => _changes;
        public Client BillOwner { get; private set; }

        public decimal CurrentAmount => GetAmountAtDate(DateTime.UtcNow);

        public ResultWithEvent Debet(decimal amount) //increment
        {
            var changeValidate = BillChanges.Create(Guid.NewGuid(), ValueObjects.BillChangeType.Simple , DateTime.UtcNow, amount);

            if (changeValidate.IsFailure)
            {
                return changeValidate.AsCommonFailureWithoutEvent();
            }

            _changes.Add(changeValidate.Value);

            return changeValidate.AsCommonWithEvent([new BillAmountChanged(this.Id, GetAmountAtDate(DateTime.UtcNow), DateTime.UtcNow)]);
        }

        public ResultWithEvent Credit(decimal amount) //decrement
        {
            if (GetAmountAtDate(DateTime.UtcNow.AddMinutes(10)) - amount < 0)
            {
                return Result.Failure("can not credit not positive bill").AsFailureWithoutEvent();
            }

            var changeValidate = BillChanges.Create(Guid.NewGuid(), ValueObjects.BillChangeType.Simple, DateTime.UtcNow, -amount);

            if (changeValidate.IsFailure)
            {
                return changeValidate.AsCommonFailureWithoutEvent();
            }

            _changes.Add(changeValidate.Value);
            
            

            return changeValidate.AsCommonWithEvent(new List<DomainEvent> {new BillAmountChanged(this.Id, this.GetAmountAtDate(DateTime.UtcNow), DateTime.UtcNow) });
        }
        public decimal GetAmountAtDate(DateTime date)
        {
            return _changes
                .Where(ex => ex.AppeandDate <= date)
                .Sum(ex => ex.Change);
        }


        private Bill(Guid id, Client owner, IList<BillChanges> changes)
        {
            Id = id;
            BillOwner = owner;
            _changes = changes.ToList();
        }

        public static ResultWithEvent<Bill> Create(Guid id, Client owner, IList<BillChanges> changes)
        {
            if(changes == default)
            {
                return Result.Failure<Bill>("empty changes list").AsFailureWithoutEvent();
            }

            if(owner == null)
            {
                return Result.Failure<Bill>("empty owner").AsFailureWithoutEvent();
            }

            return Result.Success(new Bill(id, owner, changes)).WithEvent([new BillCreatedEvent(id, owner.Id, DateTime.UtcNow)]);
        }

    }
}
