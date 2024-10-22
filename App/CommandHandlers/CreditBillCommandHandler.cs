using App.Commands;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CommandHandlers
{
    public class CreditBillCommandHandler : ICommandHandler<CreditBillCommand>
    {
        public Task<Result> HandlAsync(CreditBillCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
