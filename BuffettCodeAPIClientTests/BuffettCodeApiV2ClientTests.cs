using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Exception;

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
        private readonly BuffettCodeApiV2Client client = BuffettCodeApiV2Client.GetInstance(BuffettCodeApiConfig.TEST_API_KEY);

        /// <summary>
        /// The GetQuarterTest.
        /// </summary>
        [TestMethod()]
        public void GetQuarterTest()
        {
            uint fy = 2019;
            uint fq = 4;

            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetQuarter("6501", fy, fq, false, true, false).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetQuarter("6502", fy, fq, false, true, false));
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
            string from = "2019Q4";
            string to = "2019Q4";

            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetQuarterRange("6501", from, to, true, false).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetQuarterRange("6502", from, to, true, false));

        }


    }
}