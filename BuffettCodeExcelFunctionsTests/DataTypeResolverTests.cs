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
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("LY-1LQ"));
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("LYLQ-1"));
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("LY-1LQ-2"));
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("2021LQ-2"));
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("LY-1Q3"));

            // Quarter
            Assert.AreEqual(DataTypeConfig.Quarter, DataTypeResolver.Resolve("2020Q1"));

            // Daily
            Assert.AreEqual(DataTypeConfig.Daily, DataTypeResolver.Resolve("2020-01-01"));

            // Company
            Assert.AreEqual(DataTypeConfig.Company, DataTypeResolver.Resolve("COMPANY"));

            // Monthly
            Assert.AreEqual(DataTypeConfig.Monthly, DataTypeResolver.Resolve("201612"));
            Assert.AreEqual(DataTypeConfig.Monthly, DataTypeResolver.Resolve("201701"));

            // others
            Assert.ThrowsException<ValidationError>(() => DataTypeResolver.Resolve("dummy"));
        }
    }
}