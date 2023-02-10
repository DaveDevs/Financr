using Financr.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Financr.Tests
{
    [TestFixture]
    public class SavingsCalculatorTests
    {
        [Test]
        public void Calculate_Complex()
        {
            // A

            // A
            var total = MathHelpers.CompoundInterest(5000, 2, 300, 2).Take(30*12).Last();

            // A
            total.Should().BeApproximately(203493.77m, 0.01m);
        }

        [Test]
        public void Calculate_Basic()
        {
            // A

            // A
            var total = MathHelpers.CompoundInterest(10000, 5, 100, 0).Take(36).Last();

            // A
            total.Should().BeApproximately(15490.06m, 0.01m);
        }

        [Test]
        public void Calculate_MonthlyZero()
        {
            // A

            // A
            var total = MathHelpers.CompoundInterest(10000, 5, 100, 5).Take(36).Last();

            // A
            total.Should().BeApproximately(15697.00m, 0.01m);
        }
    }
}
