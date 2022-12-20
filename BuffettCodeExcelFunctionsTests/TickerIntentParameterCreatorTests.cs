using BuffettCodeAPIClient;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class TickerIntentParameterCreatorTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var ticker = "1234";
            var latestQuarter = FiscalQuarterPeriod.Create(2020, 2);
            // latest
            Assert.AreEqual(TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance()), TickerIntentCreator.Create(ticker, "latest", null));

            // day
            Assert.AreEqual(TickerDayParameter.Create(ticker,
                DayPeriod.Create(2020, 1, 1)), TickerIntentCreator.Create(ticker, "2020-01-01", null));


            // LYLQ
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(0, 0)), TickerIntentCreator.Create(ticker, "LYLQ", null));

            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(1, 0)), TickerIntentCreator.Create(ticker, "LY-1LQ", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(0, 2)), TickerIntentCreator.Create(ticker, "LYLQ-2", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(1, 2)), TickerIntentCreator.Create(ticker, "LY-1LQ-2", null));

            // Quarter
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2020, 1)), TickerIntentCreator.Create(ticker, "2020Q1", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "2019", "LQ-1", FiscalQuarterPeriod.Create(2019, 1)), TickerIntentCreator.Create(ticker, "2019LQ-1", latestQuarter));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "2019", "LQ-2", FiscalQuarterPeriod.Create(2018, 4)), TickerIntentCreator.Create(ticker, "2019LQ-2", latestQuarter));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "LY-2", "4", FiscalQuarterPeriod.Create(2018, 4)), TickerIntentCreator.Create(ticker, "LY-2Q4", latestQuarter));

            // Company
            Assert.AreEqual(TickerEmptyIntentParameter.Create(ticker, Snapshot.GetInstance()), TickerIntentCreator.Create(ticker, "COMPANY", null));

            // Monthly
            Assert.AreEqual(TickerYearMonthParameter.Create(ticker, YearMonthPeriod.Create(2021, 3)), TickerIntentCreator.Create(ticker, "202103", null));
            Assert.AreEqual(TickerYearMonthParameter.Create(ticker, YearMonthPeriod.Create(2022, 12)), TickerIntentCreator.Create(ticker, "202212", null));

            // others
            Assert.ThrowsException<ValidationError>(() => TickerIntentCreator.Create(ticker, "dummy", null));
        }
    }
}