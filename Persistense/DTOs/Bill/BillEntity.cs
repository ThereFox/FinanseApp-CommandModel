using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public class BillEntity
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public List<BillChangeEntity> ChangesAfterSnapshot { get; set; }
        public List<BillStateSnapshotEntitys> StateSnapshots { get; set; }

        public ClientEntity Owner { get; set; }
    }
}
