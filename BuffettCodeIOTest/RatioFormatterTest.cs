using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Formatter.UnitTests
{
    [TestClass]
    public class RatioFormatterTest
    {
        [TestMethod]
        public void TestFormat()
        {
            var formatter = RatioFormatter.GetInstance();
            var dividend_yield_forecast = new PropertyDescrption("dividend_yield_forecast", "配当利回り（会社予想）", "%");
            var dividend_yield_actual = new PropertyDescrption("dividend_yield_forecast", "配当利回り（実績）", "%");
            var others = new PropertyDescrption("any", "any", "%");
            string formatted;

            // 配当
            formatted = formatter.Format("1.23456789", dividend_yield_forecast);
            Assert.AreEqual("1.23", formatted);
            formatted = formatter.Format("9.87654321", dividend_yield_forecast);
            Assert.AreEqual("9.88", formatted);
            formatted = formatter.Format("12.3456789", dividend_yield_forecast);
            Assert.AreEqual("12.35", formatted);
            formatted = formatter.Format("0.0", dividend_yield_forecast);
            Assert.AreEqual("0.00", formatted);
            formatted = formatter.Format("1.23456789", dividend_yield_actual);
            Assert.AreEqual("1.23", formatted);
            formatted = formatter.Format("9.87654321", dividend_yield_actual);
            Assert.AreEqual("9.88", formatted);
            formatted = formatter.Format("12.3456789", dividend_yield_actual);
            Assert.AreEqual("12.35", formatted);
            formatted = formatter.Format("0.00000000", dividend_yield_actual);
            Assert.AreEqual("0.00", formatted);

            // 配当以外
            formatted = formatter.Format("1.23456789", others);
            Assert.AreEqual("1.2", formatted);
            formatted = formatter.Format("9.87654321", others);
            Assert.AreEqual("9.9", formatted);
            formatted = formatter.Format("12.3456789", others);
            Assert.AreEqual("12.3", formatted);
            formatted = formatter.Format("0.0000000", others);
            Assert.AreEqual("0.0", formatted);
        }
    }
}