using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class TickerDayParameterTests
    {
        [TestMethod()]
        public void ToApiV3ParametersTest()
        {
            var ticker = "1234";
            var parameter1 = TickerDayParameter.Create(ticker, DayPeriod.Create(2021, 1, 1)).ToApiV3Parameters();
            Assert.AreEqual("2021-01-01", parameter1["date"]);

            var parameter2 = TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance()).ToApiV3Parameters();
            Assert.AreEqual("latest", parameter2["date"]);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var ticker = "1234";
            var period = DayPeriod.Create(2021, 1, 1);
            Assert.AreEqual(period, TickerDayParameter.Create(ticker, period).GetPeriod());
            Assert.AreEqual(LatestDayPeriod.GetInstance(), TickerDayParameter.Create(ticker, "latest").GetPeriod());
            Assert.AreEqual(period, TickerDayParameter.Create(ticker, "2021-01-01").GetPeriod());

            // validation error
            Assert.ThrowsException<ValidationError>(() => TickerDayParameter.Create("dummy", period));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = TickerDayParameter.Create("1234", "2021-01-01");
            var b = TickerDayParameter.Create("1234", DayPeriod.Create(2021, 1, 1));
            var c = TickerDayParameter.Create("2345", DayPeriod.Create(2021, 1, 1));
            var d = TickerDayParameter.Create("1234", DayPeriod.Create(2021, 1, 2));
            var f = TickerDayParameter.Create("1234", LatestDayPeriod.GetInstance());
            var g = TickerDayParameter.Create("1234", LatestDayPeriod.GetInstance());
            var h = TickerDayParameter.Create("2345", LatestDayPeriod.GetInstance());

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, f);
            Assert.AreEqual(f, g);
            Assert.AreNotEqual(f, h);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = TickerDayParameter.Create("1234", "2021-01-01").GetHashCode();
            var b = TickerDayParameter.Create("1234", DayPeriod.Create(2021, 1, 1)).GetHashCode();
            var c = TickerDayParameter.Create("2345", DayPeriod.Create(2021, 1, 1)).GetHashCode();
            var d = TickerDayParameter.Create("1234", DayPeriod.Create(2021, 1, 2)).GetHashCode();
            var f = TickerDayParameter.Create("1234", LatestDayPeriod.GetInstance()).GetHashCode();
            var g = TickerDayParameter.Create("1234", LatestDayPeriod.GetInstance()).GetHashCode();
            var h = TickerDayParameter.Create("2345", LatestDayPeriod.GetInstance()).GetHashCode();

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, f);
            Assert.AreEqual(f, g);
            Assert.AreNotEqual(f, h);
        }


    }
}