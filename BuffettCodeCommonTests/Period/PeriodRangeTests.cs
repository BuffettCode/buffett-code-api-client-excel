using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class PeriodRangeTests
    {
        [TestMethod()]
        public void CreateForFiscalQuarterPeriodTest()
        {
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 2);
            var fqRange = PeriodRange<FiscalQuarterPeriod>.Create(from, to);
            Assert.AreEqual(from, fqRange.From);
            Assert.AreEqual(to, fqRange.To);
            Assert.ThrowsException<ValidationError>(() => PeriodRange<FiscalQuarterPeriod>.Create(to, from));
        }

        [TestMethod()]
        public void CreateForDayPeriodTest()
        {
            var from = DayPeriod.Create(2020, 1, 1);
            var to = DayPeriod.Create(2020, 1, 2);
            var fqRange = PeriodRange<DayPeriod>.Create(from, to);
            Assert.AreEqual(from, fqRange.From);
            Assert.AreEqual(to, fqRange.To);
            Assert.ThrowsException<ValidationError>(() => PeriodRange<DayPeriod>.Create(to, from));
        }
    }
}