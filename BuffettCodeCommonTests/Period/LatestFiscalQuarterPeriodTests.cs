using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeCommon.Period.Tests
{
    [TestClass()]
    public class LatestFiscalQuarterPeriodTests
    {
        [TestMethod()]
        public void ToV3ParameterTest()
        {
            var parameter = LatestFiscalQuarterPeriod.GetInstance().ToV3Parameter();
            Assert.AreEqual("LY", parameter["fy"]);
            Assert.AreEqual("LQ", parameter["fq"]);
        }

        [TestMethod()]
        public void ToV2ParameterTest()
        {
            var parameter = LatestFiscalQuarterPeriod.GetInstance().ToV2Parameter();
            Assert.AreEqual("LY", parameter["fy"]);
            Assert.AreEqual("LQ", parameter["fq"]);
        }
    }
}