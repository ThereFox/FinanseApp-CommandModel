using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands
{
    public class TransactionCommand : ICommand
    {
        public Guid InitBillId { get; init; }
        public Guid DestinationBillId { get; init; }
        public decimal Amount { get; init; }
    }
}
