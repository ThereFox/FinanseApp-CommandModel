using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public class BillStateSnapshotEntitys
    {
        public Guid Id { get; set; }

        public Guid BillId { get; set; }


        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

        
        public BillEntity Bill { get; set;}
    }
}
