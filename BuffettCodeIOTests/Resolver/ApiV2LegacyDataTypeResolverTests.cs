using BuffettCodeCommon.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Resolver.Tests
{
    [TestClass()]
    public class ApiV2LegacyDataTypeResolverTests
    {
        [TestMethod]
        public void TestResolve()
        {
            var resolver = Resolver.ApiV2LegacyDataTypeResolver.GetInstance();

            var quarter = resolver.Resolve("net_sales");
            Assert.AreEqual(quarter, DataTypeConfig.Quarter);

            var indicator = resolver.Resolve("roe");
            Assert.AreEqual(indicator, DataTypeConfig.Indicator);

            // 実装上、未知の項目名はQuarter扱いになる
            var others = resolver.Resolve("unknown_property");
            Assert.AreEqual(others, DataTypeConfig.Quarter);
        }
    }
}