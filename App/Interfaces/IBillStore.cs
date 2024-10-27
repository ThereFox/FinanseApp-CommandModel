using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IBillStore
    {
        public Task<Result<Bill>> GetById(Guid id);
        public Task<Result<Guid>> CreateNew(Bill entity);
        public Task<Result> SaveChanges(Bill bill);
    }
}
