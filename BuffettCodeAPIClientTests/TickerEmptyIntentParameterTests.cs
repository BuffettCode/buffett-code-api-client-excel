using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class TickerEmptyIntentParameterTests
    {
        [TestMethod()]
        public void GetPeriodTest()
        {
            var parameter = TickerEmptyIntentParameter.Create("1234", Snapshot.GetInstance());
            Assert.AreEqual(Snapshot.GetInstance(), parameter.GetIntent());
        }

        [TestMethod()]
        public void ToApiV2ParametersTest()
        {
            var ticker = "1234";
            var parameter = TickerEmptyIntentParameter.Create(ticker, Snapshot.GetInstance()).ToApiV2Parameters();
            Assert.AreEqual(1, parameter.Count());
            Assert.AreEqual(ticker, parameter["ticker"]);
        }

        [TestMethod()]
        public void ToApiV3ParametersTest()
        {
            var ticker = "1234";
            var parameter = TickerEmptyIntentParameter.Create(ticker, Snapshot.GetInstance()).ToApiV3Parameters();
            Assert.AreEqual(1, parameter.Count());
            Assert.AreEqual(ticker, parameter["ticker"]);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = TickerEmptyIntentParameter.Create("1234", Snapshot.GetInstance());
            var b = TickerEmptyIntentParameter.Create("1234", Snapshot.GetInstance());
            var c = TickerEmptyIntentParameter.Create("2345", Snapshot.GetInstance());
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = TickerEmptyIntentParameter.Create("1234", Snapshot.GetInstance()).GetHashCode();
            var b = TickerEmptyIntentParameter.Create("1234", Snapshot.GetInstance()).GetHashCode();
            var c = TickerEmptyIntentParameter.Create("2345", Snapshot.GetInstance()).GetHashCode();
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }
    }
}