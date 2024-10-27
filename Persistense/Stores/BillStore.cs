using App.Interfaces;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Persistense.DTOs;
using Persistense.DTOs.BillChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stores
{
    public class BillStore : IBillStore
    {
        private readonly ApplicationDBContext _dbContext;

        public BillStore(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<Guid>> CreateNew(Bill entity)
        {
            if(await _dbContext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<Guid>("database unawaliable");
            }


            var savable = entity.ToDTO();
            
            _dbContext.Bills.Add(savable);
            await _dbContext.SaveChangesAsync();

            return Result.Success(savable.Id);
        }

        public async Task<Result<Bill>> GetById(Guid id)
        {
            if(await _dbContext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<Bill>("database unawaliable");
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var isDatabaseContainValue = await _dbContext.Bills.AnyAsync(ex => ex.Id == id);

                if (isDatabaseContainValue == false)
                {
                    return Result.Failure<Bill>($"database dont contain value with id {id}");
                }

                var tryGetLastSnapshotResult = await _dbContext
                    .Bills
                    .AsNoTracking()
                    .Include(ex => ex.StateSnapshots)
                    .Where(ex => ex.Id == id)
                    .Select(
                        ex => ex.StateSnapshots.OrderByDescending(ex => ex.CreateDate).Last()
                    )
                    .FirstOrDefaultAsync();

                if (tryGetLastSnapshotResult == null)
                {
                    var billWithoutSnapshot = await _dbContext
                        .Bills
                        .AsNoTracking()
                        .Include(ex => ex.Owner)
                        .Include(ex => ex.ChangesAfterSnapshot)
                        .Where(ex => ex.Id == id)
                        .FirstOrDefaultAsync();

                    if (billWithoutSnapshot == default)
                    {
                        return Result.Failure<Bill>("something wrong");
                    }

                    return billWithoutSnapshot.ToDomain();
                }

                var getValueWithSnapshot = await _dbContext
                    .Bills
                    .AsNoTracking()
                    .Include(ex => ex.ChangesAfterSnapshot)
                    .Include(ex => ex.Owner)
                    .Include(ex =>
                        ex.ChangesAfterSnapshot.Where(subex => subex.CreateDate > tryGetLastSnapshotResult.CreateDate)
                    )
                    .Where(ex => ex.Id == id)
                    .FirstOrDefaultAsync();

                if (getValueWithSnapshot == default)
                {
                    return Result.Failure<Bill>("Something wrong");
                }

                getValueWithSnapshot.StateSnapshots = [tryGetLastSnapshotResult];

                await transaction.CommitAsync();

                return getValueWithSnapshot.ToDomain();

            }

        }

        public async Task<Result> SaveChanges(Bill bill)
        {
            if (await _dbContext.Database.CanConnectAsync() == false)
            {
                return Result.Failure("database unawaliable");
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var getBillResult = await _dbContext
                    .Bills
                    .Include(ex => ex.ChangesAfterSnapshot)
                    .FirstOrDefaultAsync(ex => ex.Id == bill.Id);

                if (getBillResult == default)
                {
                    return Result.Failure($"database dont contain value with id {bill.Id}");
                }

                var billChangesDTOAll = bill.ToDTO().ChangesAfterSnapshot;


                var billChangesDTO = billChangesDTOAll.Where(ex =>
                getBillResult.ChangesAfterSnapshot.Any(subex => subex.Id == ex.Id) == false).ToList();

                await _dbContext.AddRangeAsync(billChangesDTO);

                getBillResult.ChangesAfterSnapshot.AddRange(billChangesDTO);

                await _dbContext.SaveChangesAsync();


                await transaction.CommitAsync();
            }

            return Result.Success();
        }
    }
}
