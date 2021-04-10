using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Formatter.UnitTests
{
    [TestClass]
    public class CurrencyFormatterTest
    {
        [TestMethod]
        public void TestFormat()
        {
            var formatter = CurrencyFormatter.GetInstance();
            var yen = new PropertyDescrption("any", "any", "円");
            var millionYen = new PropertyDescrption("any", "any", "百万円");
            string formatted;

            // 円
            // カンマの処理だけ
            formatted = formatter.Format("1", yen);
            Assert.AreEqual("1", formatted);
            formatted = formatter.Format("10000", yen);
            Assert.AreEqual("10,000", formatted);
            formatted = formatter.Format("-1000000", yen);
            Assert.AreEqual("-1,000,000", formatted);
            formatted = formatter.Format("", yen);
            Assert.AreEqual("", formatted);

            // 百万円
            // 単位の調整もやる
            formatted = formatter.Format("1000000", millionYen);
            Assert.AreEqual("1", formatted);
            formatted = formatter.Format("1000000000", millionYen);
            Assert.AreEqual("1,000", formatted);
            formatted = formatter.Format("-1000000000000", millionYen);
            Assert.AreEqual("-1,000,000", formatted);
            formatted = formatter.Format("999999", millionYen);
            Assert.AreEqual("0", formatted);
            formatted = formatter.Format("", millionYen);
            Assert.AreEqual("", formatted);
        }
    }
}