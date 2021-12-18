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
            Assert.AreEqual(LatestFiscalQuarterPeriod.GetInstance(), PeriodResolver.Resolve("LYLQ"));

            // Quarter
            Assert.AreEqual(FiscalQuarterPeriod.Create(2020, 1), PeriodResolver.Resolve("2020Q1"));

            // Daily
            Assert.AreEqual(DayPeriod.Create(2020, 1, 1), PeriodResolver.Resolve("2020-01-01"));

            // others
            Assert.ThrowsException<ValidationError>(() => PeriodResolver.Resolve("dummy"));
        }
    }
}