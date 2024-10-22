using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys
{
    public class BillChanges : Entity<Guid>
    {
        public decimal Change { get; set; }
        public DateTime AppeandDate { get; set; }

        private BillChanges(Guid id, decimal change, DateTime createDate)
        {
            Id = id;
            Change = change;
            AppeandDate = createDate;
        }

        public static Result<BillChanges> Create(Guid id, DateTime createDate, decimal change)
        {
            return Result.Success(new BillChanges(id, change, createDate));
        }
    }
}
