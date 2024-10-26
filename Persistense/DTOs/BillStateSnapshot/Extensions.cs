using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.BillStateSnapshot
{
    public static class BillStateConvertExtensions
    {
        public static Result<BillChanges> ToDomain(this BillStateSnapshotEntitys bill)
        {
            var changeType = BillChangeType.Compilated;

            var result = BillChanges.Create(bill.Id, changeType, bill.CreateDate, bill.Amount);

            return result;
        }
    }
}
