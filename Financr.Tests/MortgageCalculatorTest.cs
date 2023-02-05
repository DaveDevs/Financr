using Financr.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Financr.Tests
{
    [TestFixture]
    public class MortgageCalculatorTest
    {
        [Test]
        public void Calculate_Low()
        {
            // A
            var mortgageCalc = new MortgageCalculator();
            mortgageCalc.PurchasePrice = 170_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A
            mortgageCalc.Calculate();

            // A
            mortgageCalc.Lbtt.Should().Be(500.00m);
            mortgageCalc.Ads.Should().Be(10200);
        }

        [Test]
        public void Calculate_Med()
        {
            // A
            var mortgageCalc = new MortgageCalculator();
            mortgageCalc.PurchasePrice = 310_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A
            mortgageCalc.Calculate();

            // A
            mortgageCalc.Lbtt.Should().Be(5100);
            mortgageCalc.Ads.Should().Be(18600);
        }

        [Test]
        public void Calculate_High()
        {
            // A
            var mortgageCalc = new MortgageCalculator();
            mortgageCalc.PurchasePrice = 510_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A
            mortgageCalc.Calculate();

            // A
            mortgageCalc.Lbtt.Should().Be(24350.00m);
            mortgageCalc.Ads.Should().Be(30600);
        }

        [Test]
        public void Calculate_Monthly()
        {
            // A
            var mortgageCalc = new MortgageCalculator();
            mortgageCalc.PurchasePrice = 510_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A
            var payment = mortgageCalc.MonthlyMortgagePayments();

            // A
            payment.Should().BeApproximately(2821.91m, 0.01m);
        }

        [Test]
        public void Calculate_Amortization()
        {
            // A
            var mortgageCalc = new MortgageCalculator();
            mortgageCalc.PurchasePrice = 200_000;
            mortgageCalc.Deposit = 0;
            mortgageCalc.InterestRate = 6;
            mortgageCalc.Years = 25;

            // A
            var payment = mortgageCalc.CalculateAmortization();

            // A
            payment.Count.Should().Be(mortgageCalc.Years * 12);
            payment.Last().EndingBalance.Should().BeApproximately(0m, 0.0001m);
        }
    }
}