using System.Reflection.Metadata.Ecma335;
using MudBlazor;

namespace Financr.Utils
{
    public class SavingsGrapher
    {
        public SavingsCalculator SavingsCalculator { get; protected set; }

        public SavingsGrapher(SavingsCalculator savingsCalculator)
        {
            SavingsCalculator = savingsCalculator;
        }

        public List<ChartSeries> Series => new List<ChartSeries>()
        {
            new ChartSeries() { Name = "Deposits", Data = this.BuildDepositsSeries() },
            new ChartSeries() { Name = "Pot", Data = this.BuildDebtSeries() },
        };

        public string[] XAxisLabels => this.BuildXAxisLabels();

        public ChartOptions Options => new ChartOptions()
        {
            YAxisTicks = 500,
            MaxNumYAxisTicks = 10,
            YAxisLines = true,
            XAxisLines = true
        };

        private double[] BuildDepositsSeries()
        {
            return Array.ConvertAll(this.SavingsCalculator.Deposits.ToArray(), x => (double)x);
        }

        private double[] BuildDebtSeries()
        {
            return Array.ConvertAll(this.SavingsCalculator.Pot.ToArray(), x => (double)x);
        }

        private string[] BuildXAxisLabels()
        {
            return new string[]{};//Array.ConvertAll(this.SavingsCalculator.AmortizationSchedule.YearlyStatements.Select(x => x.Period).ToArray(), x => x.ToString());
        }
    }
}
