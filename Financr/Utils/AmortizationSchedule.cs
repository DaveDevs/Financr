namespace Financr.Utils
{
    public class AmortizationSchedule
    {
        public int Year { get; set; }
        public decimal StartBalance { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
        public decimal EndingBalance { get; set; }
    }
}
