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

        public bool IsCompensated { get; private set; }

        public Result Do()
        {
            var doResult = _actions.DoAllBeforeError();

            if (doResult.IsFailure)
            {
                Compensate();
                return doResult;
            }

            return Result.Success();

        }

        public Result Compensate()
        {
            var compensateResult = _actions.CompensateAllBeforeError();

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
