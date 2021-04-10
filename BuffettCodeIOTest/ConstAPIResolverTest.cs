using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Resolver.UnitTests
{
    [TestClass]
    public class ConstAPIResolverTest
    {
        [TestMethod]
        public void TestResolve()
        {
            var resolver = ConstAPIResolver.GetInstance();

            var quarter = resolver.Resolve("net_sales");
            Assert.AreEqual(quarter, APIType.Quarter);

            var indicator = resolver.Resolve("roe");
            Assert.AreEqual(indicator, APIType.Indicator);

            // 実装上、未知の項目名はQuarter扱いになる
            var others = resolver.Resolve("unknown_property");
            Assert.AreEqual(others, APIType.Quarter);
        }
    }
}