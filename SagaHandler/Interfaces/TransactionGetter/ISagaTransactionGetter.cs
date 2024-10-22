using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SagaHandler.Interfaces.TransactionGetter
{
    internal interface ISagaTransactionGetter
    {
        internal IDbTransaction GetTransaction();
    }
}
