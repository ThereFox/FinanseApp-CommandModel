using App.Commands;
using App.Interfaces;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CommandHandlers
{
    public class DebetBillCommandHandler : ICommandHandler<DebetBillCommand>
    {
        private readonly IBillStore _billStore;

        public DebetBillCommandHandler(IBillStore billStore)
        {
            _billStore = billStore;
        }

        public async Task<Result> HandlAsync(DebetBillCommand command)
        {
            var getBillResult = await _billStore.GetById(command.BillId);

            if (getBillResult.IsFailure)
            {
                return getBillResult;
            }

            var bill = getBillResult.Value;

            var debetResult = bill.Debet(command.Amount);

            if (debetResult.IsSuccess)
            {
                await _billStore.SaveChanges(bill);
            }

            return getBillResult;
        }
    }
}
