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
    public class CreditBillCommandHandler : ICommandHandler<CreditBillCommand>
    {
        private readonly IBillStore _billStore;

        public CreditBillCommandHandler(IBillStore store)
        {
            _billStore = store;
        }

        public async Task<Result> HandlAsync(CreditBillCommand command)
        {
            var getBillResult = await _billStore.GetById(command.BillId);

            if (getBillResult.IsFailure)
            {
                return getBillResult;
            }

            var bill = getBillResult.Value;

            var creditResult = bill.Credit(command.BillChange);

            if (creditResult.IsSuccess)
            {
                await _billStore.SaveChanges(bill);
            }

            return creditResult;
        }
    }
}
