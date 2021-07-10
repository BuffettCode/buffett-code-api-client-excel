using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    /// <summary>
    /// Defines the <see cref="BuffettCodeApiV3ClientTests" />.
    /// </summary>
    [TestClass()]
    public class BuffettCodeApiV3ClientTests
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private readonly BuffettCodeApiV3Client client = BuffettCodeApiV3Client.GetInstance
            (BuffettCodeApiKeyConfig.TestApiKey);

        /// <summary>
        /// The GetDailyTest.
        /// </summary>
        [TestMethod()]
        public void GetDailyTest()
        {
            // test api key can get toyota data
            var day = DayPeriod.Create(2021, 2, 1);
            Assert.IsNotNull(client.GetDaily("7203", day, false, true, false).Result);

            // test api key can get not toyota
            Assert.ThrowsExceptionAsync<InvalidAPIKeyException>
                (() => client.GetDaily("6502", day, true, true, false));
        }
    }
}