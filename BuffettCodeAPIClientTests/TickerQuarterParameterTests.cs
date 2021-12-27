using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class TickerQuarterParameterTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var ticker = "1234";
            // fiscal quarter period
            var quarter = FiscalQuarterPeriod.Create(2020, 1);
            Assert.AreEqual("2020Q1",
                TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2020, 1)).GetPeriod().ToString());

            Assert.AreEqual("2020Q1",
                TickerQuarterParameter.Create(ticker, "LY-1", "LQ-20", FiscalQuarterPeriod.Create(2020, 1)).GetPeriod().ToString());


            Assert.AreEqual("LYLQ",
                TickerQuarterParameter.Create(ticker, RelativeFiscalQuarterPeriod.Create(0, 0)).GetPeriod().ToString());

            // validation checks
            Assert.ThrowsException<ValidationError>(() => TickerQuarterParameter.Create("dummy", "LY", "LQ", quarter));
            Assert.ThrowsException<ValidationError>(() => TickerQuarterParameter.Create(ticker, "ly", "LQ", quarter));
            Assert.ThrowsException<ValidationError>(() => TickerQuarterParameter.Create(ticker, "LY", "lq", quarter));
            Assert.ThrowsException<ValidationError>(() => TickerQuarterParameter.Create(ticker, "LY", "6", quarter));
        }

        [TestMethod()]
        public void ToApiV2ParametersTest()
        {
            var ticker = "1234";
            var parameter1 = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2020, 1)).ToApiV2Parameters();

            Assert.AreEqual(ticker, parameter1["ticker"]);
            Assert.AreEqual("2020", parameter1["fy"]);
            Assert.AreEqual("1", parameter1["fq"]);

            var parameter2 = TickerQuarterParameter.Create(ticker, "LY", "LQ", FiscalQuarterPeriod.Create(2020, 1)).ToApiV2Parameters();
            Assert.AreEqual(ticker, parameter1["ticker"]);
            Assert.AreEqual("LY", parameter2["fy"]);
            Assert.AreEqual("LQ", parameter2["fq"]);
        }

        [TestMethod()]
        public void ToApiV3ParametersTest()
        {
            var ticker = "1234";
            var parameter1 = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(2020, 1)).ToApiV3Parameters();

            Assert.AreEqual(ticker, parameter1["ticker"]);
            Assert.AreEqual("2020", parameter1["fy"]);
            Assert.AreEqual("1", parameter1["fq"]);


            var parameter2 = TickerQuarterParameter.Create(ticker, "LY", "LQ", FiscalQuarterPeriod.Create(2020, 1)).ToApiV3Parameters();
            Assert.AreEqual(ticker, parameter2["ticker"]);
            Assert.AreEqual("LY", parameter2["fy"]);
            Assert.AreEqual("LQ", parameter2["fq"]);
        }

        [TestMethod()]
        public void GetPeriodTest()
        {
            var ticker = "1234";
            var period = FiscalQuarterPeriod.Create(2020, 1);

            Assert.AreEqual(period, TickerQuarterParameter.Create(ticker, period).GetPeriod());
            Assert.AreEqual(period, TickerQuarterParameter.Create(ticker, "LY", "LQ", period).GetPeriod());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Create(2020, 1));
            var b = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Parse("2020Q1"));
            var c = TickerQuarterParameter.Create("2345", FiscalQuarterPeriod.Create(2020, 1));
            var d = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Create(2020, 2));
            var e = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 2));
            var f = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 2));
            var g = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 3));
            var i = TickerQuarterParameter.Create("2345", RelativeFiscalQuarterPeriod.Create(1, 2));
            var j = TickerQuarterParameter.Create("2345", "2019", "LQ-2", RelativeFiscalQuarterPeriod.Create(1, 2));

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
            Assert.AreEqual(e, f);
            Assert.AreNotEqual(e, g);
            Assert.AreNotEqual(e, i);
            Assert.AreNotEqual(e, g);
            Assert.AreNotEqual(e, j);
        }


        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Create(2020, 1)).GetHashCode();
            var b = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Parse("2020Q1")).GetHashCode();
            var c = TickerQuarterParameter.Create("2345", FiscalQuarterPeriod.Create(2020, 1)).GetHashCode();
            var d = TickerQuarterParameter.Create("1234", FiscalQuarterPeriod.Create(2020, 2)).GetHashCode();
            var e = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 2)).GetHashCode();
            var f = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 2)).GetHashCode();
            var g = TickerQuarterParameter.Create("1234", RelativeFiscalQuarterPeriod.Create(1, 3)).GetHashCode();
            var i = TickerQuarterParameter.Create("2345", RelativeFiscalQuarterPeriod.Create(1, 2)).GetHashCode();
            var j = TickerQuarterParameter.Create("2345", "2019", "LQ-2", RelativeFiscalQuarterPeriod.Create(1, 2)).GetHashCode();

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
            Assert.AreEqual(e, f);
            Assert.AreNotEqual(e, g);
            Assert.AreNotEqual(e, i);
            Assert.AreNotEqual(e, g);
            Assert.AreNotEqual(e, j);

        }
    }
}