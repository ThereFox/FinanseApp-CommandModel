using App.CommandHandlers;
using App.Commands;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Infrastructure.SagaHandler.Interfaces.TransactionGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.Interfaces.SagaActions
{
    public sealed class DebetBillSagaAction : ISagaAction
    {
        private readonly ICommandHandler<DebetBillCommand> _actionHandler;
        private readonly ICommandHandler<CreditBillCommand> _compensateHandler;

        private readonly Guid _billId;
        private readonly decimal _amount;

        public bool IsExecuted { get; private set; }
        public bool IsCompensated { get; private set; }

        public async Task<Result> CompensateAsync()
        {
            if(IsExecuted == false || IsCompensated)
            {
                return Result.Success();
            }

            var compensateCommand = new CreditBillCommand(_billId, _amount);

            return await _compensateHandler.HandlAsync(compensateCommand);
        }

        public async Task<Result> DoAsync()
        {
            if (IsExecuted)
            {
                return Result.Success();
            }

            var command = new DebetBillCommand(_billId, _amount);

            return await _actionHandler.HandlAsync(command);

        }
    }
}
