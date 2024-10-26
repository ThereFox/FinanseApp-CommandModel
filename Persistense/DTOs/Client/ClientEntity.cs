using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<BillEntity> Bills { get; set; }
    }
}
