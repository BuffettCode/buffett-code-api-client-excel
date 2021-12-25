using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class BuffettCodeApiV3ClientTests
    {
        private readonly ApiClientCoreWithCache mockApiCore = ApiClientTestHelper.CreateMockApiClientCoreWithCache("dummy");

        [TestMethod()]
        public void GetDailyTest()
        {
            var parameter = TickerDayParameter.Create("6501", DayPeriod.Create(2021, 2, 1));
            var client = new BuffettCodeApiV3Client(mockApiCore);
            Assert.IsNotNull(client.GetDaily(parameter, false, true, false));
        }

        [TestMethod()]
        public void GetQuarterTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var parameter = TickerQuarterParameter.Create("6501", FiscalQuarterPeriod.Create(2019, 4));
            Assert.IsNotNull(client.GetQuarter(parameter, false, true, false));
        }

        [TestMethod()]
        public void GetQuarterRangeTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var from = FiscalQuarterPeriod.Create(2019, 4);
            var to = FiscalQuarterPeriod.Create(2019, 4);
            var parameter = TickerPeriodRangeParameter.Create("6501", from, to);
            Assert.IsNotNull(client.GetQuarterRange(parameter, false, false));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var parameter = TickerEmptyPeriodParameter.Create("6501", Snapshot.GetInstance());
            Assert.IsNotNull(client.GetCompany(parameter, true, false));
        }

        [TestMethod()]
        public void GetRangeTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var day = DayPeriod.Create(2021, 2, 1);
            var fyFq = FiscalQuarterPeriod.Create(2019, 4);
            var ticker = "2345";

            Assert.IsNotNull(client.GetRange(DataTypeConfig.Quarter, TickerPeriodRangeParameter.Create(ticker, fyFq, fyFq), false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.GetRange(DataTypeConfig.Daily, TickerPeriodRangeParameter.Create(ticker, day, day), false, true, false));
        }

        [TestMethod()]
        public void GetTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var ticker = "2345";
            var day = TickerDayParameter.Create(ticker, DayPeriod.Create(2021, 2, 1));
            var fyFq = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2019, 4));
            var snapshot = TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance());

            Assert.IsNotNull(client.Get(DataTypeConfig.Daily, day, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Quarter, fyFq, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Company, snapshot, false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.Get(DataTypeConfig.Indicator, snapshot, false, true, false));
        }
    }
}