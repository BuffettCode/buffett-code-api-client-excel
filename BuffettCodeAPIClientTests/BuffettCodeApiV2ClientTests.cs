using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuffettCodeAPIClient;

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
        private BuffettCodeApiV2Client client = new BuffettCodeApiV2Client(ApiTestConfig.TEST_API_KEY);

        /// <summary>
        /// The GetQuarterTest.
        /// </summary>
        [TestMethod()]
        public void GetQuarterTest()
        {
            uint fy = 2019;
            uint fq = 4;

            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetQuarter("6501", fy, fq, false, true).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetQuarter("6502", fy, fq, false, true));
        }

        [TestMethod()]
        public void GetIndicatorTest()
        {
            // test api key can get ticker=xx01
            Assert.IsNotNull(client.GetIndicator("6501", true).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>(() => client.GetIndicator("6502", true));
        }
    }
}
