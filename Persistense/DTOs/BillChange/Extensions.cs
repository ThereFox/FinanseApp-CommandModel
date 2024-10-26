using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.BillChange
{
    public static class Extensions
    {
        public static BillChangeEntity ToDTO(this BillChanges billChange)
        {
            var result = new BillChangeEntity()
            {
                Id = billChange.Id,
                Change = billChange.Change,
                CreateDate = billChange.AppeandDate
            };
            return result;
        }
        public static Result<BillChanges> ToDomain(this BillChangeEntity bill)
        {
            var changeValue = BillChangeType.Simple;

            var validateResult = BillChanges.Create(bill.Id, changeValue, bill.CreateDate, bill.Change);

            return validateResult;

        }
    }
}
