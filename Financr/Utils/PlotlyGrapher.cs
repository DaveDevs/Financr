using Plotly.Blazor;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor.Traces;
using Plotly.Blazor.Traces.ScatterLib;

namespace Financr.Utils
{
    public class PlotlyGrapher
    {
        private IList<ITrace> _data;
        private static string _hoverTemplate = "£ %{y:,.2f} <br> Month %{x}";

        public LoanCalculator LoanCalculator { get; protected set; }

        public PlotlyGrapher(LoanCalculator loanCalculator)
        {
            Chart = new PlotlyChart();
            _data = new List<ITrace>();
            LoanCalculator = loanCalculator;
            Config = new Config(){ AutoSizable = true, Responsive = true };
            Layout = new Layout(){ Margin = new Margin(){AutoExpand = false, B=20,L=50,R=10,T=10}};
        }

        public PlotlyChart Chart;
        public Config Config { get; set; }
        public Layout Layout { get; set; } 
        
        public IList<ITrace> Data {
            get => BuildChart();
            set => _data = value;
        }

        public IList<ITrace> BuildChart()
        {
            _data.Clear();
            _data.Add(new Scatter
            {
                Name = "Balance",
                Mode = ModeFlag.Lines | ModeFlag.Markers,
                Y = BuildBalanceSeries(),
                HoverInfo = HoverInfoFlag.Y,
                HoverTemplate = _hoverTemplate,
            });
            _data.Add(new Scatter
            {
                Name = "Interest",
                Mode = ModeFlag.Lines | ModeFlag.Markers,
                Y = BuildDebtSeries(),
                HoverInfo = HoverInfoFlag.Y,
                HoverTemplate = _hoverTemplate,
            });
            _data.Add(new Scatter
            {
                Name = "Total",
                Mode = ModeFlag.Lines | ModeFlag.Markers,
                Y = BuildPaymentSeries(),
                HoverInfo = HoverInfoFlag.Y,
                HoverTemplate = _hoverTemplate,
            });
            return _data;
        }
        private IList<object> BuildBalanceSeries()
        {
            List<decimal> balance = new List<decimal> { this.LoanCalculator.MortgageAmount };
            balance.AddRange(this.LoanCalculator.AmortizationSchedule.MonthlyStatements.Select(x => x.EndingBalance.BankerRound()));

            var foo = balance.Cast<object>().ToList();
            return foo;
        }

        private IList<object> BuildDebtSeries()
        {
            IList<decimal> debtPaid = new List<decimal>();
            decimal runningTotalDebt = 0;
            debtPaid.Add(runningTotalDebt);
            foreach (var yearlyStatement in this.LoanCalculator.AmortizationSchedule.MonthlyStatements)
            {
                runningTotalDebt += yearlyStatement.Interest;
                debtPaid.Add(runningTotalDebt.BankerRound());
            }

            return debtPaid.Cast<object>().ToList();
        }

        private IList<object> BuildPaymentSeries()
        {
            IList<decimal> totalPayments = new List<decimal>();
            decimal runningTotalPayment = 0;
            totalPayments.Add(runningTotalPayment);
            foreach (var yearlyStatement in this.LoanCalculator.AmortizationSchedule.MonthlyStatements)
            {
                runningTotalPayment += LoanCalculator.MonthlyPayment;
                totalPayments.Add(runningTotalPayment.BankerRound());
            }
            return totalPayments.Cast<object>().ToList();
        }
    }

    public static class DecimalUtils
    {
        public static decimal BankerRound(this decimal foo)
        {
            return decimal.Round(foo, 2, MidpointRounding.AwayFromZero);
        }
    }
}
 