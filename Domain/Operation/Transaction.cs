using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Operation
{
    public class Transaction : Entity<Guid>
    {
        private List<BillChanges> _changes;

        public bool IsExecuted { get; }
        public Bill From { get; }
        public Bill To { get; }
        public decimal Amount { get; init; }

        private Transaction(Guid id, bool isExecuted, Bill initId, Bill destinationId, decimal amount)
        {
            Id = id;
            IsExecuted = isExecuted;
            From = initId;
            To = destinationId;
            Amount = amount;
        }

        public Result Execute()
        {
            if (IsExecuted)
            {
                return Result.Failure("Transaction already executed");
            }

            if (From.GetAmountAtDate(DateTime.Now) < Amount)
            {
                return Result.Failure("dont have amount in init bill");
            }

            var creditResult = From.Credit(Amount, this);

            if (creditResult.IsFailure)
            {
                return creditResult;
            }

            var debetResult = To.Debet(Amount, this);

            if (debetResult.IsFailure)
            {
                From.Debet(Amount);
                return debetResult;
            }

            return Result.Success();
        }

        public static Result<Transaction> Create(Guid id, bool isExecuted, Bill from, Bill to, decimal amount)
        {

            return Result.Success(new Transaction(id, isExecuted, from, to, amount));
        }

    }
}
