using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands
{
    public class CreditBillCommand : ICommand
    {
        public Guid BillId { get; init; }
        public decimal BillChange {  get; init; }

        public CreditBillCommand(Guid billId, decimal billChange)
        {
            BillId = billId;
            BillChange = billChange;
        }

    }
}
