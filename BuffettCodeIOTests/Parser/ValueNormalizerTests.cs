using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ValueNormalizerTests
    {
        [TestMethod()]
        public void NormalizeTest()
        {
            Assert.AreEqual("", ValueNormalizer.Normalize(null));
            Assert.AreEqual("", ValueNormalizer.Normalize("None"));
            Assert.AreEqual("dummy", ValueNormalizer.Normalize("dummy"));
        }
    }
}