using SagaHandler.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaHandler.Interfaces
{
    public interface ISageTransactionStore
    {
        public SagaTransaction GetTransaction(Guid transactionId);
        public void SaveTransaction(SagaTransaction transaction);
    }
}
