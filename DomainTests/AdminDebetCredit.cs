using Domain.Entitys;
using DomainTests.Factoryes;

namespace DomainTests
{
    public class AdminDebetCredit
    {
        [Theory]
        [InlineData(500, 100)]
        [InlineData(0, 100)]
        [InlineData(-100, 100)]
        public void Should_Success_BillDebetIncrementAmount(int initAmount, int DebetAmount)
        {
            //arrange
            var bill = new BillBuilder()
                .WithInitAmount(initAmount, DateTime.Now)
                .Build();

            var debetResult = bill.Debet(DebetAmount);

            Assert.True(debetResult.IsSuccess);
            Assert.Equal(initAmount + DebetAmount, bill.GetAmountAtDate(DateTime.Now.AddMinutes(5)));
        }

        [Theory]
        [InlineData(500, 100)]
        [InlineData(100, 100)]
        public void Should_Success_BillCreditDecremetAmount(int initAmount, int CreditAmount)
        {
            //arrange
            var bill = new BillBuilder()
                .WithInitAmount(initAmount, DateTime.Now)
                .Build();

            var debetResult = bill.Credit(CreditAmount);

            Assert.True(debetResult.IsSuccess);
            Assert.Equal(initAmount - CreditAmount, bill.GetAmountAtDate(DateTime.Now.AddMinutes(5)));
        }

        [Fact]
        public void Should_Fail_BillCreditWithZeroAmount()
        {
            //arrange
            var bill = new BillBuilder()
                .WithInitAmount(0, DateTime.Now)
                .Build();

            var debetResult = bill.Credit(100);

            Assert.True(debetResult.IsFailure);
        }

    }
}