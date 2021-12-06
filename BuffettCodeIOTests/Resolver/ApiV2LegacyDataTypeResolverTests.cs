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
            var resolver = ApiV2LegacyDataTypeResolver.GetInstance();

            // quarter にしかないもの
            Assert.AreEqual(resolver.Resolve("net_sales"), DataTypeConfig.Quarter);

            // quarter, indicator 両方にあるものは quarter を優先する
            Assert.AreEqual(resolver.Resolve("roe"), DataTypeConfig.Quarter);

            // indicator のみにあるもの
            Assert.AreEqual(resolver.Resolve("eps_forecast"), DataTypeConfig.Indicator);

            // 実装上、未知の項目名はQuarter扱いになる
            var others = resolver.Resolve("unknown_property");
            Assert.AreEqual(others, DataTypeConfig.Quarter);
        }
    }
}