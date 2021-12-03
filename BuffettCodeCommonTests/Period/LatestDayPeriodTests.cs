using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class LatestDayPeriodTests
    {
        [TestMethod()]
        public void ToV3ParameterTest()
        {
            var parameter = LatestDayPeriod.GetInstance().ToV3Parameter();
            Assert.AreEqual("latest", parameter["date"]);
        }
    }
}