using CSharpFunctionalExtensions;
using Domain.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys
{
    public class BillChanges : Entity<Guid>
    {
        public Transaction? ExecutedInContext { get; init; }
        public Bill ChangedBill { get; init; }

        public decimal Change { get; init; }
        public DateTime AppeandDate { get; init; }

        private BillChanges(Guid id, Bill bill, decimal change, DateTime createDate)
        {
            Id = id;
            ChangedBill = bill;
            Change = change;
            AppeandDate = createDate;
        }
        private BillChanges(Guid id, Bill bill, decimal change, DateTime createDate, Transaction executionContext)
        {
            Id = id;
            ChangedBill = bill;
            Change = change;
            AppeandDate = createDate;
            ExecutedInContext = executionContext;
        }

        public static Result<BillChanges> Create(Guid id, Bill bill, DateTime createDate, decimal change)
        {
            return Result.Success(new BillChanges(id, bill, change, createDate));
        }
        public static Result<BillChanges> Create(Guid id, Bill bill, DateTime createDate, decimal change, Transaction transaction)
        {
            if(transaction.From != bill && transaction.To != bill)
            {
                return Result.Failure<BillChanges>("Transaction not contain this bill");
            }

            return Result.Success(new BillChanges(id, bill, change, createDate, transaction));
        }
    }
}
