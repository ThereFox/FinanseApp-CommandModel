using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands
{
    public class DebetBillCommand : ICommand
    {
        public Guid BillId { get; init; }
        public decimal Amount { get; init; }

        public DebetBillCommand(Guid billId, decimal amount)
        {
            BillId = billId;
            Amount = amount;
        }
    }
}
