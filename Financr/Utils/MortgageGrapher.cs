using System.Reflection.Metadata.Ecma335;
using MudBlazor;

namespace Financr.Utils
{
    public class MortgageGrapher
    {
        public MortgageCalculator MortgageCalculator { get; protected set; }

        public MortgageGrapher(MortgageCalculator mortgageCalculator)
        {
            MortgageCalculator = mortgageCalculator;
        }

        public List<ChartSeries> Series => new List<ChartSeries>()
        {
            new ChartSeries() { Name = "Balance", Data = this.BuildBalanceSeries() },
            new ChartSeries() { Name = "Interest", Data = this.BuildDebtSeries() },
            new ChartSeries() { Name = "Payment", Data = this.BuildPaymentSeries() },
        };

        public string[] XAxisLabels => this.BuildXAxisLabels();

        public ChartOptions Options => new ChartOptions()
        {
            YAxisTicks = 10000,
            MaxNumYAxisTicks = 10,
            YAxisLines = true,
            XAxisLines = true,
        };

        private double[] BuildBalanceSeries()
        {
            List<decimal> balance = new List<decimal> { this.MortgageCalculator.MortgageAmount };
            balance.AddRange(this.MortgageCalculator.AmortizationSchedule.YearlyStatements.Select(x => x.EndingBalance));

            return Array.ConvertAll(balance.ToArray(), x => (double)x);
        }

        private double[] BuildDebtSeries()
        {
            IList<decimal> debtPaid = new List<decimal>();
            decimal runningTotalDebt = 0;
            debtPaid.Add(runningTotalDebt);
            foreach (var yearlyStatement in this.MortgageCalculator.AmortizationSchedule.YearlyStatements)
            {
                runningTotalDebt += yearlyStatement.Interest;
                debtPaid.Add(runningTotalDebt);
            }
            return Array.ConvertAll(debtPaid.ToArray(), x => (double)x);
        }

        private double[] BuildPaymentSeries()
        {
            IList<decimal> totalPayments = new List<decimal>();
            decimal runningTotalPayment = 0;
            totalPayments.Add(runningTotalPayment);
            foreach (var yearlyStatement in this.MortgageCalculator.AmortizationSchedule.YearlyStatements)
            {
                runningTotalPayment += MortgageCalculator.MonthlyPayment * 12;
                totalPayments.Add(runningTotalPayment);
            }
            return Array.ConvertAll(totalPayments.ToArray(), x => (double)x);
        }

        private string[] BuildXAxisLabels()
        {
            return Array.ConvertAll(this.MortgageCalculator.AmortizationSchedule.YearlyStatements.Select(x => x.Period).ToArray(), x => x.ToString());
        }
    }
}
