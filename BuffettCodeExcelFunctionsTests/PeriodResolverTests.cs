using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class PeriodResolverTests
    {
        [TestMethod()]
        public void ResolveTest()
        {
            // latest
            Assert.AreEqual(LatestDayPeriod.GetInstance(), PeriodResolver.Resolve("latest"));

            // LYLQ
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(0, 0), PeriodResolver.Resolve("LYLQ"));
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(1, 0), PeriodResolver.Resolve("LY-1LQ"));
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(0, 2), PeriodResolver.Resolve("LYLQ-2"));
            Assert.AreEqual(RelativeFiscalQuarterPeriod.Create(1, 2), PeriodResolver.Resolve("LY-1LQ-2"));

            // Quarter
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 1), PeriodResolver.Resolve("2020Q1"));

            // Daily
            Assert.AreEqual(DayPeriod.Create(2020, 1, 1), PeriodResolver.Resolve("2020-01-01"));

            // others
            Assert.ThrowsException<ValidationError>(() => PeriodResolver.Resolve("dummy"));
        }
    }
}