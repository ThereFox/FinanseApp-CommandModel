using Domain.Operation;
using DomainTests.Factoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests
{
    public class Transactions
    {
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(10)]
        [InlineData(1)]
        public void Should_Sucsess_TransactionExecuteConsistensy(decimal initAmount)
        {
            var initBill = new BillBuilder().WithInitAmount(initAmount, DateTime.Now).Build();
            var destinationBill = new BillBuilder().WithInitAmount(0, DateTime.Now).Build();
            var transaction = Transaction.Create(Guid.NewGuid(), false, initBill, destinationBill, initAmount);

            var executeResult = transaction.Value.Execute();

            Assert.True(executeResult.IsSuccess);
            Assert.True(initBill.GetAmountAtDate(DateTime.Now) == 0);
            Assert.True(destinationBill.GetAmountAtDate(DateTime.Now) == initAmount);
        }

        [Theory]
        [InlineData(100, 200)]
        [InlineData(1, 2)]
        public void Should_Failure_TransactionFromBillBelowAmount(decimal initAmount, decimal amountChange)
        {
            var initBill = new BillBuilder().WithInitAmount(initAmount, DateTime.Now).Build();
            var destinationBill = new BillBuilder().WithInitAmount(0, DateTime.Now).Build();
            var transaction = Transaction.Create(Guid.NewGuid(), false, initBill, destinationBill, amountChange);

            var executeResult = transaction.Value.Execute();

            Assert.True(executeResult.IsFailure);
            Assert.True(initBill.GetAmountAtDate(DateTime.Now) == initAmount);
            Assert.True(destinationBill.GetAmountAtDate(DateTime.Now) == 0);
        }

    }
}
