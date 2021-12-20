using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class SupportedTierRangeTests
    {
        [TestMethod()]
        public void FixedTierRengeLengthTest()
        {
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2021, 2);
            var range = PeriodRange<FiscalQuarterPeriod>.Create(from, to);

            var supportedTierRange = new SupportedTierRange<FiscalQuarterPeriod>(range, null);

            Assert.AreEqual((uint)5, supportedTierRange.FixedTierRengeLength());
        }

        [TestMethod()]
        public void OndemandTierRengeLengthTest()
        {
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2021, 2);
            var range = PeriodRange<FiscalQuarterPeriod>.Create(from, to);

            var supportedTierRange = new SupportedTierRange<FiscalQuarterPeriod>(null, range);

            Assert.AreEqual((uint)5, supportedTierRange.OndemandTierRengeLength());
        }
    }
}