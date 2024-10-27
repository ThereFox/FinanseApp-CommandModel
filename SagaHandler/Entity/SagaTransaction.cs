using CSharpFunctionalExtensions;
using Domain.Entitys;
using Infrastructure.SagaHandler.Extensions;
using Infrastructure.SagaHandler.Interfaces.SagaActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaHandler.Entity
{
    public class SagaTransaction
    {
        private readonly List<ISagaAction> _actions;

        public bool IsExecuted { get; private set; }
        public bool IsCompensated { get; private set; }

        public async Task<Result> DoAsync()
        {
            var doResult = await _actions.DoAllAsyncBeforeError();

            if (doResult.IsFailure)
            {
                await CompensateAsync();
                return doResult;
            }

            return Result.Success();

        }

        public async Task<Result> CompensateAsync()
        {
            var compensateResult = await _actions.CompensateAllAsyncBeforeError();

            if (compensateResult.IsFailure)
            {
                return compensateResult;
            }

            return Result.Success();

        }

        protected SagaTransaction()
        {
            
        }

    }
}
