using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class BuffettCodeApiV2ClientTests
    {

        private readonly ApiClientCoreWithCache mockApiCore = ApiClientTestHelper.CreateMockApiClientCoreWithCache("dummy");


        [TestMethod()]
        public void GetQuarterTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var parameter = TickerQuarterParameter.Create("6501", FiscalQuarterPeriod.Create(2019, 4));
            Assert.IsNotNull(client.GetQuarter(parameter, false, true, false));
        }

        [TestMethod()]
        public void GetIndicatorTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var parameter = TickerEmptyPeriodParameter.Create("6501", Snapshot.GetInstance());
            Assert.IsNotNull(client.GetIndicator(parameter, true));
        }

        [TestMethod()]
        public void GetQuarterRangeTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var from = FiscalQuarterPeriod.Create(2019, 4);
            var to = FiscalQuarterPeriod.Create(2019, 4);
            var parameter = TickerPeriodRangeParameter.Create("6501", from, to);
            Assert.IsNotNull(client.GetQuarterRange(parameter, false, false));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var parameter = TickerEmptyPeriodParameter.Create("6501", Snapshot.GetInstance());
            Assert.IsNotNull(client.GetCompany(parameter, true, false));
        }

        [TestMethod()]
        public void GetTest()
        {
            var ticker = "2345";
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var day = TickerDayParameter.Create(ticker, DayPeriod.Create(2021, 2, 1));
            var fyFq = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2019, 4));
            var empty = TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance());

            Assert.IsNotNull(client.Get(DataTypeConfig.Indicator, empty, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Quarter, fyFq, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Company, empty, false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.Get(DataTypeConfig.Daily, day, false, true, false));
        }

        [TestMethod()]
        public void GetRangeTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var ticker = "2345";
            var day = DayPeriod.Create(2021, 2, 1);
            var fyFq = FiscalQuarterPeriod.Create(2019, 4);

            Assert.IsNotNull(client.GetRange(DataTypeConfig.Quarter, TickerPeriodRangeParameter.Create(ticker, fyFq, fyFq), false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.GetRange(DataTypeConfig.Daily, TickerPeriodRangeParameter.Create(ticker, day, day), false, true, false));
        }

    }
}