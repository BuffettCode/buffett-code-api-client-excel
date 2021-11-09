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
            var period = FiscalQuarterPeriod.Create(2019, 4);
            Assert.IsNotNull(client.GetQuarter("6501", period, false, true, false));
        }

        [TestMethod()]
        public void GetIndicatorTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            Assert.IsNotNull(client.GetIndicator("6501", true));
        }

        [TestMethod()]
        public void GetQuarterRangeTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            var from = FiscalQuarterPeriod.Create(2019, 4);
            var to = FiscalQuarterPeriod.Create(2019, 4);
            Assert.IsNotNull(client.GetQuarterRange("6501", from, to, false, false));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            var client = new BuffettCodeApiV2Client(mockApiCore);
            Assert.IsNotNull(client.GetCompany("6501", true, false));
        }


    }
}