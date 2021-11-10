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
    }
}