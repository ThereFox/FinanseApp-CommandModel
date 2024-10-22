using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.Interfaces.SagaActions
{
    public interface ISagaAction
    {
        public Task<Result> DoAsync();
        public Task<Result> CompensateAsync();
    }
}
