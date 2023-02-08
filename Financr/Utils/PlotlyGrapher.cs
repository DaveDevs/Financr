using Plotly.Blazor;
using Plotly.Blazor.Traces;
using Plotly.Blazor.Traces.ScatterLib;

namespace Financr.Utils
{
    public class PlotlyGrapher
    {
        public PlotlyGrapher()
        {
            Chart = new PlotlyChart();
            this.Data = new List<ITrace>();
        }

        public PlotlyChart Chart;
        public Config Config = new Config();
        public Layout Layout = new Layout();
        // Using of the interface IList is important for the event callback!
        public IList<ITrace> Data { get; protected set; }

        public async Task BuildChart()
        {
            this.Data.Clear();
            List<object> foo = quote.Select(x => x.Open).Cast<object>().ToList();
            this.Data.Add(new Scatter
            {
                Name = "ScatterTrace",
                Mode = ModeFlag.Lines | ModeFlag.Markers,
                Y = foo
            });
        }
    }
}
