using App.CommandHandlers;
using App.Commands;
using Infrastructure.SagaHandler.UseCases.UseCases;
using SagaHandler.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.UseCases
{
    public class SagaTransactionCreditCommandHandler : ICommandHandler<SagaTransactionCommand<CreditBillCommand>>
    {
        private readonly ICommandHandler<CreditBillCommand> _commandHandler;

        public void Handl(SagaTransactionCommand<CreditBillCommand> command)
        {
            _commandHandler.Handl(command.SubCommand);
        }
    }
}
