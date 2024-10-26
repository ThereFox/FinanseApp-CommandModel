using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using Persistense.DTOs.BillChange;
using Persistense.DTOs.BillStateSnapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public static class BillEntityExtensions
    {
        public static BillEntity ToDTO(this Bill bill)
        {
            var newSnapshot = new BillStateSnapshotEntitys()
            {
                Amount = bill.CurrentAmount,
                CreateDate = DateTime.UtcNow,
                BillId = bill.Id
            };

            var changes = bill
                .Changes
                .Where(ex => ex.Type == BillChangeType.Simple)
                .Select(ex => ex.ToDTO())
                .ToList();

            changes.ForEach(ex => ex.BillId = bill.Id);

            var result = new BillEntity()
            {
                Id = bill.Id,
                OwnerId = bill.BillOwner.Id,
                ChangesAfterSnapshot = changes
            };

            return result;
        }
        public static BillEntity ToDTOWithUpdateState(this Bill bill)
        {
            var newSnapshot = new BillStateSnapshotEntitys()
            {
                Amount = bill.CurrentAmount,
                CreateDate = DateTime.UtcNow,
                BillId = bill.Id
            };

            var result = new BillEntity()
            {
                Id = bill.Id,
                OwnerId = bill.BillOwner.Id,
                StateSnapshots = [newSnapshot],
                ChangesAfterSnapshot = []
            };

            return result;
        }
        public static Result<Bill> ToDomain(this BillEntity bill)
        {
            var validateClientResult = Domain.Entitys.Client.Create(bill.Owner.Id, bill.Owner.Name);

            Domain.Entitys.Client client;

            if (validateClientResult.IsFailure)
            {
                client = null;
            }
            else
            {
                client = validateClientResult.Value;
            }

            List<BillChanges> changes = new List<BillChanges>();

            if (bill.StateSnapshots != null && bill.StateSnapshots.Any())
            {
                var snapshotChangeValidate = bill.StateSnapshots.OrderByDescending(ex => ex.CreateDate).Last().ToDomain();

                if (snapshotChangeValidate.IsFailure)
                {
                    return Result.Failure<Bill>("invalid snapshot");
                }
                changes.Add(snapshotChangeValidate.Value);
            }

            if (bill.ChangesAfterSnapshot != null && bill.ChangesAfterSnapshot.Any())
            {
                var validatedChanges = bill.ChangesAfterSnapshot.ConvertAll(ex => ex.ToDomain());

                if (validatedChanges.Any(ex => ex.IsFailure))
                {
                    return Result.Failure<Bill>("invalid changes");
                }
                changes.AddRange(validatedChanges.Select(ex => ex.Value));
            }


            var billResult = Domain.Entitys.Bill.Create(bill.Id, client, changes);

            return billResult;
        }
    }
}
