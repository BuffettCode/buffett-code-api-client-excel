using BuffettCodeCommon.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ApiResponseParserFactoryTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            Assert.IsInstanceOfType(ApiResponseParserFactory.Create(BuffettCodeApiVersion.Version2), typeof(ApiV2ResponseParser));
            Assert.IsInstanceOfType(ApiResponseParserFactory.Create(BuffettCodeApiVersion.Version3), typeof(ApiV3ResponseParser));
        }
    }
}