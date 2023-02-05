using System.Globalization;

namespace Financr.Utils
{
    public class AmortizationStatement
    {
        public int Period { get; set; }
        public decimal StartBalance { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
        public decimal EndingBalance { get; set; }

        public static string FormatDecimal(decimal toFormat)
        {
            CultureInfo ci = new CultureInfo("en-GB");
            ci.NumberFormat.CurrencySymbol = "£";

            var rounded = decimal.Round(toFormat, 2, MidpointRounding.AwayFromZero);
            return rounded.ToString("C", ci);
        }
    }

    public class AmortizationSchedule
    {
        public AmortizationSchedule(IList<AmortizationStatement> statements)
        {
            this.MonthlyStatements = statements;
            this.YearlyStatements = new List<AmortizationStatement>();
            this.BuildYearlyStatement();
        }

        public IList<AmortizationStatement> MonthlyStatements { get; set; }

        public IList<AmortizationStatement> YearlyStatements { get; set; }

        private void BuildYearlyStatement()
        {
            var years = MonthlyStatements.Count / 12;

            for (int i = 0; i < years; i++)
            {
                var yearStatements = this.MonthlyStatements.Skip(i*12).Take(12).ToList();
                this.YearlyStatements.Add(new AmortizationStatement()
                {
                    Period = i + 1,
                    StartBalance = yearStatements.First().StartBalance,
                    EndingBalance = yearStatements.Last().EndingBalance,
                    Interest = yearStatements.Sum(x => x.Interest),
                    Principal = yearStatements.Sum(x => x.Principal)
                });
            }
        }
    }
}
