using CSharpFunctionalExtensions;
using Domain.Operation;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys
{
    public class BillChanges : Entity<Guid>
    {
        public BillChangeType Type { get; init; }
        public decimal Change { get; init; }
        public DateTime AppeandDate { get; init; }

        private BillChanges(Guid id, BillChangeType type, decimal change, DateTime createDate)
        {
            Id = id;
            Change = change;
            Type = type;
            AppeandDate = createDate;
        }

        public static Result<BillChanges> Create(Guid id, BillChangeType type, DateTime createDate, decimal change)
        {
            return Result.Success(new BillChanges(id, type, change, createDate));
        }
    }
}
