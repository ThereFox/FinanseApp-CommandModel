using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }
        
        public Guid InitBillId { get; set; }
        public Guid DestinationBillId { get; set; }

        public bool IsApplyed { get; set; }
        public DateTime CreateDate { get; set; }

        public BillEntity FromBill { get; set; }
        public BillEntity ToBill { get; set; }
        public List<BillChangeEntity> Changes { get; set; }

    }
}
