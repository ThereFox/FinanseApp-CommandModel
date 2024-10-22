using App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.UseCases.UseCases
{
    public class SagaTransactionCommand<TActionCommand> : ICommand
        where TActionCommand : ICommand
    {
        public Guid ExternTransactionId { get; set; }
        public TActionCommand SubCommand { get; set; }
    }
}
