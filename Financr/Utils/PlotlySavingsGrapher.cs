using Plotly.Blazor;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor.Traces;
using Plotly.Blazor.Traces.ScatterLib;

namespace Financr.Utils
{
    public class PlotlySavingsGrapher
    {
        private IList<ITrace> _data;
        private static string _hoverTemplate = "£ %{y:,.2f} <br> Month %{x}";

        public SavingsCalculator SavingsCalculator { get; protected set; }

        public PlotlySavingsGrapher(SavingsCalculator savingsCalculator)
        {
            Chart = new PlotlyChart();
            _data = new List<ITrace>();
            SavingsCalculator = savingsCalculator;
            Config = new Config(){ AutoSizable = true, Responsive = true };
            Layout = new Layout()
            {
                BarMode = BarModeEnum.Overlay,
                Margin = new Margin(){AutoExpand = false, B=20,L=50,R=10,T=10}
            };
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
            _data.Add(new Bar
            {
                Name = "Pot",
                Y = BuildPotSeries(),
                HoverInfo = Plotly.Blazor.Traces.BarLib.HoverInfoFlag.Y,
                HoverTemplate = _hoverTemplate,
            });
            _data.Add(new Bar()
            {
                Name = "Deposited",
                Y = BuildDepositsSeries(),
                HoverInfo = Plotly.Blazor.Traces.BarLib.HoverInfoFlag.Y,
                HoverTemplate = _hoverTemplate,
            });
            return _data;
        }
        private IList<object> BuildDepositsSeries()
        {
            var bar = this.SavingsCalculator.Deposits.Select(x => x.BankerRound()).Cast<object>().ToList();
            return bar;
        }

        private IList<object> BuildPotSeries()
        {
            var bar = this.SavingsCalculator.Pot.Select(x => x.BankerRound()).Cast<object>().ToList();
            return bar;
        }
    }
}
 