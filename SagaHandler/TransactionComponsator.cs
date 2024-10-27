using App.Interfaces;
using SagaHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaHandler
{
    public class TransactionComponsator
    {
        private readonly ISageTransactionStore _store;

        public void Compensate(Guid transactionId)
        {
            var transactionInfo = _store.GetTransaction(transactionId);

        }
    }
}
