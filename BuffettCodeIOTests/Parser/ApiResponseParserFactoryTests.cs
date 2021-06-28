using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
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
            Assert.ThrowsException<NotSupportedDataTypeException>(() => ApiResponseParserFactory.Create(BuffettCodeApiVersion.Version3));
        }
    }
}