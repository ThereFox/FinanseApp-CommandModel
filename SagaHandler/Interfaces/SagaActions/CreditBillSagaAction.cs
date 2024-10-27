using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.Interfaces.SagaActions
{
    internal class CreditBillSagaAction : ISagaAction
    {
        public Task<Result> CompensateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result> DoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
