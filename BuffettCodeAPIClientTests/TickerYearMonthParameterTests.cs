using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass]
    public class TickerYearMonthParameterTests
    {
        [TestMethod]
        public void CreateTest()
        {
            var ticker = "1234";
            var param = TickerYearMonthParameter.Create(ticker, YearMonthPeriod.Create(2022, 1));
            Assert.AreEqual(ticker, param.GetTicker());
            Assert.AreEqual("202201", param.GetIntent().ToString());
        }

        [TestMethod()]
        public void ToApiV3ParametersTest()
        {
            var ticker = "1234";
            var parameter1 = TickerYearMonthParameter.Create(ticker, YearMonthPeriod.Create(2017, 12)).ToApiV3Parameters();

            Assert.AreEqual(ticker, parameter1["ticker"]);
            Assert.AreEqual("2017", parameter1["year"]);
            Assert.AreEqual("12", parameter1["month"]);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = TickerYearMonthParameter.Create("1234", YearMonthPeriod.Create(2015, 1));
            var b = TickerYearMonthParameter.Create("1234", YearMonthPeriod.Create(2015, 1));
            var c = TickerYearMonthParameter.Create("1234", YearMonthPeriod.Create(2015, 12));
            var d = TickerYearMonthParameter.Create("1234", YearMonthPeriod.Create(2018, 5));
            var e = TickerYearMonthParameter.Create("1234", YearMonthPeriod.Create(2018, 1));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
            Assert.AreNotEqual(c, b);
            Assert.AreNotEqual(c, d);
            Assert.AreNotEqual(c, e);
            Assert.AreNotEqual(d, e);
        }
    }
}