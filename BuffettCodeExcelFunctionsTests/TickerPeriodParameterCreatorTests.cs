using BuffettCodeAPIClient;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class TickerPeriodParameterCreatorTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var ticker = "1234";
            var latestQuarter = FiscalQuarterPeriod.Create(2020, 2);
            // latest
            Assert.AreEqual(TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance()), TickerPeriodParameterCreator.Create(ticker, "latest", null));

            // day
            Assert.AreEqual(TickerDayParameter.Create(ticker,
                DayPeriod.Create(2020, 1, 1)), TickerPeriodParameterCreator.Create(ticker, "2020-01-01", null));


            // LYLQ
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(0, 0)), TickerPeriodParameterCreator.Create(ticker, "LYLQ", null));

            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(1, 0)), TickerPeriodParameterCreator.Create(ticker, "LY-1LQ", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(0, 2)), TickerPeriodParameterCreator.Create(ticker, "LYLQ-2", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(1, 2)), TickerPeriodParameterCreator.Create(ticker, "LY-1LQ-2", null));

            // Quarter
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2020, 1)), TickerPeriodParameterCreator.Create(ticker, "2020Q1", null));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "2019", "LQ-1", FiscalQuarterPeriod.Create(2019, 1)), TickerPeriodParameterCreator.Create(ticker, "2019LQ-1", latestQuarter));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "2019", "LQ-2", FiscalQuarterPeriod.Create(2018, 4)), TickerPeriodParameterCreator.Create(ticker, "2019LQ-2", latestQuarter));
            Assert.AreEqual(TickerQuarterParameter.Create(ticker, "LY-2", "4", FiscalQuarterPeriod.Create(2018, 4)), TickerPeriodParameterCreator.Create(ticker, "LY-2Q4", latestQuarter));

            // others
            Assert.ThrowsException<ValidationError>(() => TickerPeriodParameterCreator.Create(ticker, "dummy", null));
        }
    }
}