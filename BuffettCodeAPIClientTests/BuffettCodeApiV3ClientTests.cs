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
            var day = DayPeriod.Create(2021, 2, 1);
            var client = new BuffettCodeApiV3Client(mockApiCore);
            Assert.IsNotNull(client.GetDaily("6501", day, false, true, false));
        }

        [TestMethod()]
        public void GetQuarterTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var period = FiscalQuarterPeriod.Create(2019, 4);
            Assert.IsNotNull(client.GetQuarter("6501", period, false, true, false));
        }

        [TestMethod()]
        public void GetQuarterRangeTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var from = FiscalQuarterPeriod.Create(2019, 4);
            var to = FiscalQuarterPeriod.Create(2019, 4);
            Assert.IsNotNull(client.GetQuarterRange("6501", from, to, false, false));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            Assert.IsNotNull(client.GetCompany("6501", true, false));
        }

        [TestMethod()]
        public void GetRangeTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var day = DayPeriod.Create(2021, 2, 1);
            var fyFq = FiscalQuarterPeriod.Create(2019, 4);
            var ticker = "2345";

            Assert.IsNotNull(client.GetRange(DataTypeConfig.Quarter, ticker, fyFq, fyFq, false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.GetRange(DataTypeConfig.Daily, ticker, day, day, false, true, false));
        }

        [TestMethod()]
        public void GetTest()
        {
            var client = new BuffettCodeApiV3Client(mockApiCore);
            var day = DayPeriod.Create(2021, 2, 1);
            var fyFq = FiscalQuarterPeriod.Create(2019, 4);
            var ticker = "2345";

            Assert.IsNotNull(client.Get(DataTypeConfig.Daily, ticker, day, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Quarter, ticker, fyFq, false, true, false));
            Assert.IsNotNull(client.Get(DataTypeConfig.Company, ticker, Snapshot.GetInstance(), false, true, false));
            Assert.ThrowsException<NotSupportedDataTypeException>(() => client.Get(DataTypeConfig.Indicator, ticker, Snapshot.GetInstance(), false, true, false));
        }
    }
}