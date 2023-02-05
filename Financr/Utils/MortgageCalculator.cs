using static MudBlazor.Colors;

namespace Financr.Utils
{

    public class MortgageCalculator
    {
        public decimal PurchasePrice { get; set; }
        public decimal Deposit { get; set; }
        public decimal MortgageAmount => this.PurchasePrice - Deposit;
        public decimal InterestRate { get; set; }
        public decimal PercentageRate => this.InterestRate * 0.01m;
        public decimal MonthlyInterestRate => this.PercentageRate / 12;
        public int Years { get; set; }
        public int Months => this.Years * 12;
        public decimal Lbtt => CalculateLbtt();
        public decimal Ads => this.PurchasePrice / 100 * 6;
        public decimal Total => this.CalculateTotal();
        public AmortizationSchedule AmortizationSchedule => this.CalculateAmortization();
        public decimal MonthlyPayment => this.MonthlyMortgagePayments();

        public AmortizationSchedule CalculateAmortization()
        {
            var statements = new List<AmortizationStatement>();
            var previousMonth = new AmortizationStatement();

            previousMonth.Period = 0;
            previousMonth.StartBalance = this.MortgageAmount;
            previousMonth.Interest = this.MortgageAmount * this.MonthlyInterestRate;
            previousMonth.Principal = this.MonthlyPayment - previousMonth.Interest;
            previousMonth.EndingBalance = this.MortgageAmount - previousMonth.Principal;

            statements.Add(previousMonth);

            for (int i = 1; i < this.Months; i++)
            {
                var currentMonth = new AmortizationStatement();

                currentMonth.Period = i;
                currentMonth.StartBalance = previousMonth.EndingBalance;
                currentMonth.Interest = previousMonth.EndingBalance * this.MonthlyInterestRate;
                currentMonth.Principal = this.MonthlyPayment - currentMonth.Interest;
                currentMonth.EndingBalance = previousMonth.EndingBalance - currentMonth.Principal;

                statements.Add(currentMonth);
                previousMonth = currentMonth;
            }

            return new AmortizationSchedule(statements);
        }

        private decimal MonthlyMortgagePayments()
        {
            var p = (double)this.MortgageAmount;
            var i = (double)this.InterestRate / 100 / 12;
            var n = this.Years * 12;

            return (decimal)(p * (i * Math.Pow(1 + i, n)) / ((Math.Pow(1 + i, n) - 1)));
        }

        private decimal CalculateTotal()
        {
            var total = 0m;

            if (this.PurchasePrice <= 40000)
            {
                total = this.PurchasePrice;
                return total;
            }

            total = this.PurchasePrice + this.Lbtt + this.Ads;

            return total;
        }

        private decimal CalculateLbtt()
        {
            var lbbt = 0m;
            if (this.PurchasePrice > 145000)
            {
                lbbt += (Math.Min(this.PurchasePrice, 250000) - 145000) / 100 * 2;
            }

            if (this.PurchasePrice > 250000)
            {
                lbbt += (Math.Min(this.PurchasePrice, 325000) - 250000) / 100 * 5;
            }

            if (this.PurchasePrice > 325000)
            {
                lbbt += (Math.Min(this.PurchasePrice, 750000) - 325000) / 100 * 10;
            }

            if (this.PurchasePrice > 750000)
            {
                lbbt += (this.PurchasePrice - 750000) / 100 * 12;
            }

            return lbbt;
        }
    }
}

