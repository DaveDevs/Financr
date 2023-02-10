namespace Financr.Utils
{
    public class SavingsCalculator
    {
        public decimal StartingBalance { get; set; }
        public decimal ExpectedAnnualReturn { get; set; }
        public decimal ExpectedMonthlyDeposit { get; set; }
        public decimal ExpectedMonthlyDepositIncrease { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        public int TotalMonths => this.Years * 12 + Months;

        public IEnumerable<Decimal> Pot => 
            MathHelpers.CompoundInterest(
                this.StartingBalance, 
                this.ExpectedAnnualReturn,
                this.ExpectedMonthlyDeposit, 
                this.ExpectedMonthlyDepositIncrease).Take(this.TotalMonths);
        public IEnumerable<Decimal> Deposits =>
            MathHelpers.CompoundInterest(
                this.StartingBalance,
                0,
                this.ExpectedMonthlyDeposit,
                this.ExpectedMonthlyDepositIncrease).Take(this.TotalMonths);
    }

    public static class MathHelpers
    {
        public static decimal BankerRound(this decimal foo)
        {
            return decimal.Round(foo, 2, MidpointRounding.AwayFromZero);
        }

        public static IEnumerable<decimal> CompoundInterest(decimal startingBalance, decimal expectedAnnualReturn, decimal expectedMonthlyDeposit, decimal expectedAnnualDepositIncrease)
        {
            var monthlyInterest = 1 + expectedAnnualReturn / (1200);
            var currentAmount = startingBalance;
            
            var annualInterest = 1 + expectedAnnualDepositIncrease / (100);
            var adjustedMonthlyAmount = expectedMonthlyDeposit;
            var months = 0;
            while (true)
            {
                months++;
                if (months % 12 == 0)
                {
                    adjustedMonthlyAmount *= annualInterest;
                }
                currentAmount = currentAmount * monthlyInterest + adjustedMonthlyAmount;
                yield return currentAmount;
            }
        }
    }
}
