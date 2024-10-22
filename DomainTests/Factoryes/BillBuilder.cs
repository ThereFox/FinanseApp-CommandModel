using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests.Factoryes
{
    public class BillBuilder
    {
        private List<BillChanges> _changes { get; set; }
        private Client _client { get; set; }

        public BillBuilder WithInitAmount(decimal amount, DateTime actionDate)
        {
            _changes = new List<BillChanges>();

            var billAmount = BillChanges.Create(actionDate, amount);

            if (billAmount.IsFailure)
            {
                throw new InvalidCastException("Несоответствие логики билдера и доменной логики");
            }

            _changes = new List<BillChanges>() { billAmount.Value };

            return this;
        }

        public BillBuilder WithClient(Client client)
        {
            _client = client;
            return this;
        }

        public Bill Build()
        {

            if(_client == default)
            {
                _client = Client.Create(Guid.NewGuid(), "test client").Value;
            }

            var bill = Bill.Create(Guid.NewGuid(), _client, _changes);

            if (bill.IsFailure)
            {
                throw new InvalidCastException("Несоответствие логики билдера и доменной логики");
            }

            return bill.Value;
        }
    }
}
