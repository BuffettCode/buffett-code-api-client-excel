using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Formatter.Tests
{
    [TestClass()]
    public class MillionYenFormatterTests
    {
        [TestMethod()]
        public void FormatTest()
        {
            var formatter = MillionYenFormatter.GetInstance();
            var desc = new PropertyDescription("any", "any", "百万円");

            // 四捨五入で小数点以下1桁に丸める
            Assert.AreEqual("100", formatter.Format("100,000,000", desc));
            Assert.AreEqual("100,000", formatter.Format("100,000,000,000", desc));
            Assert.AreEqual("-10", formatter.Format("-10,000,000.1", desc));
            Assert.AreEqual("0.1", formatter.Format("100,000", desc));
            Assert.AreEqual("0.2", formatter.Format("150,000.0", desc));
        }
    }
}