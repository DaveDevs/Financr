using Financr.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Financr.Tests
{
    [TestFixture]
    public class LoanCalculatorTest
    {
        [Test]
        public void Calculate_Low()
        {
            // A
            var mortgageCalc = new LoanCalculator();
            mortgageCalc.PurchasePrice = 170_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A

            // A
            mortgageCalc.Lbtt.Should().Be(500.00m);
        }

        [Test]
        public void Calculate_Med()
        {
            // A
            var mortgageCalc = new LoanCalculator();
            mortgageCalc.PurchasePrice = 310_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A

            // A
            mortgageCalc.Lbtt.Should().Be(5100);
        }

        [Test]
        public void Calculate_High()
        {
            // A
            var mortgageCalc = new LoanCalculator();
            mortgageCalc.PurchasePrice = 510_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A

            // A
            mortgageCalc.Lbtt.Should().Be(24350.00m);
        }

        [Test]
        public void Calculate_Monthly()
        {
            // A
            var mortgageCalc = new LoanCalculator();
            mortgageCalc.PurchasePrice = 510_000;
            mortgageCalc.Deposit = 10_000;
            mortgageCalc.InterestRate = 4.65m;
            mortgageCalc.Years = 25;

            // A

            // A
            mortgageCalc.MonthlyPayment.Should().BeApproximately(2821.91m, 0.01m);
        }

        [Test]
        public void Calculate_Amortization()
        {
            // A
            var mortgageCalc = new LoanCalculator();
            mortgageCalc.PurchasePrice = 200_000;
            mortgageCalc.Deposit = 0;
            mortgageCalc.InterestRate = 6;
            mortgageCalc.Years = 25;

            // A
            var schedule = mortgageCalc.CalculateAmortization();

            // A
            schedule.MonthlyStatements.Count.Should().Be(mortgageCalc.Years * 12);
            schedule.YearlyStatements.Count.Should().Be(mortgageCalc.Years);

            schedule.MonthlyStatements.Last().EndingBalance.Should().BeApproximately(0m, 0.0001m);
            
            schedule.YearlyStatements.Last().EndingBalance.Should().BeApproximately(0m, 0.0001m);
            schedule.YearlyStatements.Last().Interest.Should().BeApproximately(491.03m, 0.02m);
        }
    }
}