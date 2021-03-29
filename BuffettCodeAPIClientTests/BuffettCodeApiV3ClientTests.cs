namespace BuffettCodeAPIClient.Tests
{
    using BuffettCodeAPIClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Defines the <see cref="BuffettCodeApiV3ClientTests" />.
    /// </summary>
    [TestClass()]
    public class BuffettCodeApiV3ClientTests
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private BuffettCodeApiV3Client client = new BuffettCodeApiV3Client(ApiTestConfig.TEST_API_KEY);

        /// <summary>
        /// The GetDailyTest.
        /// </summary>
        [TestMethod()]
        public void GetDailyTest()
        {
            // test api key can get ticker=xx01 data
            var day = new DateTime(2021, 2, 1);
            Assert.IsNotNull(client.GetDaily("6501", day, false, true).Result);

            // test api key can get ticker=xx02
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>
                (() => client.GetDaily("6502", day, true, true));
        }
    }
}
