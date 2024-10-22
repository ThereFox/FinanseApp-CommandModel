using CSharpFunctionalExtensions;
using Infrastructure.SagaHandler.Interfaces.SagaActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.Extensions
{
    public static class ExecuteBeforeErrorExtension
    {
        public static Result DoAllBeforeError(this List<ISagaAction> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            {
                var executeResult = actions[i].Do();

                if (executeResult.IsFailure)
                {
                    return executeResult;
                }
            }

            return Result.Success();
        }

        public static Result CompensateAllBeforeError(this List<ISagaAction> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            {
                var executeResult = actions[i].Compensate();

                if (executeResult.IsFailure)
                {
                    return executeResult;
                }
            }

            return Result.Success();
        }

    }
}
