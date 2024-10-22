using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Operation
{
    public class Transaction : Entity<Guid>
    {
        private readonly List<BillChanges> _changes;

        public Bill From { get; }
        public Bill To { get; }

        private Transaction(List<BillChanges> actions)
        {
            _changes = actions;
        }

        public static Result<Transaction> Create()
        {
            throw new NotImplementedException();
        }

    }
}
