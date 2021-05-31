using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Formatter.Tests
{
    [TestClass()]
    public class NumericFormatterTests
    {
        [TestMethod]
        public void TestFormat()
        {
            var formatter = NumericFormatter.GetInstance();
            var numeric = new PropertyDescription("any", "any", "株");
            string formatted;

            // 整数
            formatted = formatter.Format("1", numeric);
            Assert.AreEqual("1", formatted);
            formatted = formatter.Format("10000", numeric);
            Assert.AreEqual("10,000", formatted);
            formatted = formatter.Format("-1000000", numeric);
            Assert.AreEqual("-1,000,000", formatted);
            formatted = formatter.Format("0", numeric);
            Assert.AreEqual("0", formatted);
            formatted = formatter.Format("", numeric);
            Assert.AreEqual("", formatted);

            // 小数
            // 四捨五入で小数点以下1桁に丸める
            formatted = formatter.Format("1.23456789", numeric);
            Assert.AreEqual("1.2", formatted);
            formatted = formatter.Format("12345.6789", numeric);
            Assert.AreEqual("12,345.7", formatted);
            formatted = formatter.Format("-999999.9", numeric);
            Assert.AreEqual("-999,999.9", formatted);
            formatted = formatter.Format("123.00000000", numeric);
            Assert.AreEqual("123", formatted);
            formatted = formatter.Format("0.0", numeric);
            Assert.AreEqual("0", formatted);
        }
    }
}