using static MudBlazor.Colors;

namespace Financr.Utils
{

    public class MortgageCalculator
    {
        public double PurchasePrice { get; set; }
        public double Deposit { get; set; }
        public double InterestRate { get; set; }
        public int Years { get; set; }
        public double Lbtt { get; set; }
        public double Ads { get; set; }
        public double Total { get; set; }

        public double MonthlyMortgagePayments()
        {
            var p = this.PurchasePrice - this.Deposit;
            var i = this.InterestRate / 100 / 12;
            var n = this.Years * 12;

            return p * (i * Math.Pow(1 + i, n)) / ((Math.Pow(1 + i, n) - 1));
        }

        public void Calculate()
        {
            this.Lbtt = 0;
            this.Ads = 0;
            this.Total = 0;

            if (this.PurchasePrice <= 40000)
            {
                this.Total = this.PurchasePrice;
                return;
            }

            CalculateLbtt();

            this.Ads = this.PurchasePrice / 100 * 6;

            this.Total = this.PurchasePrice + this.Lbtt + this.Ads;
        }

        private void CalculateLbtt()
        {
            if (this.PurchasePrice > 145000)
            {
                this.Lbtt += (Math.Min(this.PurchasePrice, 250000) - 145000) / 100 * 2;
            }

            if (this.PurchasePrice > 250000)
            {
                this.Lbtt += (Math.Min(this.PurchasePrice, 325000) - 250000) / 100 * 5;
            }

            if (this.PurchasePrice > 325000)
            {
                this.Lbtt += (Math.Min(this.PurchasePrice, 750000) - 325000) / 100 * 10;
            }

            if (this.PurchasePrice > 750000)
            {
                this.Lbtt += (this.PurchasePrice - 750000) / 100 * 12;
            }
        }
    }
}

