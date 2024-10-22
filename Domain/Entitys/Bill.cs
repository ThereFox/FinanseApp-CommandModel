using CSharpFunctionalExtensions;
using Domain.Operation;

namespace Domain.Entitys
{
    public sealed class Bill : Entity<Guid>
    {
        private readonly List<BillChanges> _changes;

        public Client BillOwner { get; private set; }

        public Result Debet(decimal amount) //increment
        {
            var changeValidate = BillChanges.Create(Guid.NewGuid(), this, DateTime.Now, amount);

            if (changeValidate.IsFailure)
            {
                return changeValidate;
            }

            _changes.Add(changeValidate.Value);

            return Result.Success();
        }

        public Result Debet(decimal amount, Transaction executionContext) //increment
        {
            var changeValidate = BillChanges.Create(Guid.NewGuid(), this, DateTime.Now, amount, executionContext);

            if (changeValidate.IsFailure)
            {
                return changeValidate;
            }

            _changes.Add(changeValidate.Value);

            return Result.Success();
        }

        public Result Credit(decimal amount) //decrement
        {
            if (GetAmountAtDate(DateTime.Now.AddMinutes(10)) - amount < 0)
            {
                return Result.Failure("can not credit not positive bill");
            }

            var changeValidate = BillChanges.Create(Guid.NewGuid(), this, DateTime.Now, -amount);

            if (changeValidate.IsFailure)
            {
                return changeValidate;
            }

            _changes.Add(changeValidate.Value);

            return Result.Success();
        }
        public Result Credit(decimal amount, Transaction executionContext) //decrement
        {
            if (GetAmountAtDate(DateTime.Now.AddMinutes(10)) - amount < 0)
            {
                return Result.Failure("can not credit not positive bill");
            }

            var changeValidate = BillChanges.Create(Guid.NewGuid(), this, DateTime.Now, -amount, executionContext);

            if (changeValidate.IsFailure)
            {
                return changeValidate;
            }

            _changes.Add(changeValidate.Value);

            return Result.Success();
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

        public static Result<Bill> Create(Guid id, Client owner, IList<BillChanges> changes)
        {
            if(changes == default)
            {
                return Result.Failure<Bill>("empty changes list");
            }

            if(owner == null)
            {
                return Result.Failure<Bill>("empty owner");
            }

            return Result.Success(new Bill(id, owner, changes));
        }

    }
}
