using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Persistense.DTOs
{
    public class BillChangeEntity
    {
        public Guid Id {  get; set; }

        public Guid BillId { get; set; }
        public Nullable<Guid> TransactionId { get; set; }

        public decimal Change { get; set; }
        public DateTime CreateDate { get; set; }

        public BillEntity Bill { get; set; }

        public TransactionEntity? ExecutionContext { get; set; }
    }
}
