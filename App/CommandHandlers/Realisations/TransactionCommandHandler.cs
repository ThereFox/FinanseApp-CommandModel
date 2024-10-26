using App.Commands;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CommandHandlers.Realisations
{
    public class TransactionCommandHandler : ICommandHandler<TransactionCommand>
    {
        public Task<Result> HandlAsync(TransactionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
