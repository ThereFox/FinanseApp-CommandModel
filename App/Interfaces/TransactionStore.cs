using CSharpFunctionalExtensions;
using Domain.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface ITransactionStore
    {
        public Task<Result<Transaction>> GetTransactionById(Guid id);
    }
}
