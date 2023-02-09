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
            var loanCalc = new LoanCalculator();
            loanCalc.PurchasePrice = 170_000;
            loanCalc.Deposit = 10_000;
            loanCalc.InterestRate = 4.65m;
            loanCalc.Years = 25;
            loanCalc.IsMortgage = true;

            // A

            // A
            loanCalc.Lbtt.Should().Be(500.00m);
        }

        [Test]
        public void Calculate_Med()
        {
            // A
            var loanCalc = new LoanCalculator();
            loanCalc.PurchasePrice = 310_000;
            loanCalc.Deposit = 10_000;
            loanCalc.InterestRate = 4.65m;
            loanCalc.Years = 25;
            loanCalc.IsMortgage = true;

            // A

            // A
            loanCalc.Lbtt.Should().Be(5100);
        }

        [Test]
        public void Calculate_High()
        {
            // A
            var loanCalc = new LoanCalculator();
            loanCalc.PurchasePrice = 510_000;
            loanCalc.Deposit = 10_000;
            loanCalc.InterestRate = 4.65m;
            loanCalc.Years = 25;
            loanCalc.IsMortgage = true;

            // A

            // A
            loanCalc.Lbtt.Should().Be(24350.00m);
        }

        [Test]
        public void Calculate_Monthly()
        {
            // A
            var loanCalc = new LoanCalculator();
            loanCalc.PurchasePrice = 510_000;
            loanCalc.Deposit = 10_000;
            loanCalc.InterestRate = 4.65m;
            loanCalc.Years = 25;
            loanCalc.IsMortgage = true;

            // A

            // A
            loanCalc.MonthlyPayment.Should().BeApproximately(2821.91m, 0.01m);
        }

        [Test]
        public void Calculate_Amortization()
        {
            // A
            var loanCalc = new LoanCalculator();
            loanCalc.PurchasePrice = 200_000;
            loanCalc.Deposit = 0;
            loanCalc.InterestRate = 6;
            loanCalc.Years = 25;
            loanCalc.IsMortgage = true;

            // A
            var schedule = loanCalc.CalculateAmortization();

            // A
            schedule.MonthlyStatements.Count.Should().Be(loanCalc.Years * 12);
            schedule.YearlyStatements.Count.Should().Be(loanCalc.Years);

            schedule.MonthlyStatements.Last().EndingBalance.Should().BeApproximately(0m, 0.0001m);
            
            schedule.YearlyStatements.Last().EndingBalance.Should().BeApproximately(0m, 0.0001m);
            schedule.YearlyStatements.Last().Interest.Should().BeApproximately(491.03m, 0.02m);
        }
    }
}