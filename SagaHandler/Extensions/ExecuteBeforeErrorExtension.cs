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
        public static async Task<Result> DoAllAsyncBeforeError(this List<ISagaAction> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            {
                var executeResult = await actions[i].DoAsync();

                if (executeResult.IsFailure)
                {
                    return executeResult;
                }
            }

            return Result.Success();
        }

        public static async Task<Result> CompensateAllAsyncBeforeError(this List<ISagaAction> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            {
                var executeResult = await actions[i].CompensateAsync();

                if (executeResult.IsFailure)
                {
                    return executeResult;
                }
            }

            return Result.Success();
        }

    }
}
