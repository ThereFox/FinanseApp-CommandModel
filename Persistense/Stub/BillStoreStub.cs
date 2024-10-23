using App.Interfaces;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stub
{
    public class BillStoreStub : IBillStore
    {
        public async Task<Result<Bill>> GetById(Guid id)
        {
            return Bill.Create(Guid.NewGuid(), Client.Create(Guid.Empty, "5").Value, new List<BillChanges>());
        }

        public async Task<Result> SaveChanges(Bill bill)
        {
            return Result.Success();
        }
    }
}
