using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class DataTypeResolverTests
    {
        [TestMethod()]
        public void ResolveTest()
        {
            // latest
            Assert.AreEqual(DataTypeConfig.Daily, DataTypeResolver.Resolve("latest"));

            // LYLQ
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("LYLQ"));

            // Quarter
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("2020Q1"));

            // Daily
            Assert.AreEqual(DataTypeConfig.Daily, DataTypeResolver.Resolve("2020-01-01"));

            // others
            Assert.ThrowsException<ValidationError>(() => DataTypeResolver.Resolve("dummy"));
        }
    }
}