using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;

namespace BuffettCodeAPIClient.Tests
{
    using BuffettCodeAPIClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines the <see cref="BuffettCodeApiV2ClientTests" />.
    /// </summary>
    [TestClass()]
    public class BuffettCodeApiV2ClientTests
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private readonly BuffettCodeApiV2Client client = BuffettCodeApiV2Client.GetInstance(BuffettCodeApiKeyConfig.TestApiKey);

        /// <summary>
        /// The GetQuarterTest.
        /// </summary>
        [TestMethod()]
        public void GetQuarterTest()
        {
            var period = FiscalQuarterPeriod.Create(2019, 4);
            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetQuarter("6501", period, false, true, false).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetQuarter("6502", period, false, true, false));
        }

        [TestMethod()]
        public void GetIndicatorTest()
        {
            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetIndicator("6501", true).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetIndicator("6502", true, false));
        }

        [TestMethod()]
        public void GetQuarterRangeTest()
        {
            var from = FiscalQuarterPeriod.Create(2019, 4);
            var to = FiscalQuarterPeriod.Create(2019, 4);

            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetQuarterRange("6501", from, to, true, false).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetQuarterRange("6502", from, to, true, false));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetCompany("6501", true, true).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetCompany("6502", true, false));
        }


    }
}