using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Commands.Realisations
{
    public class CreateBillCommand : ICommand
    {
        public Guid OwnerId { get; set; }
    }
}
