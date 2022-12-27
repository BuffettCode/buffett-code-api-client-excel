using BuffettCodeExcelFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuffettCodeExcelFunctionsTests
{
    [TestClass]
    public class PropertyNameResolverTests
    {
        [TestMethod]
        public void ResolveTest()
        {
            Assert.AreEqual(PropertyNameResolver.Resolve("ticker"), "ticker");
            Assert.AreEqual(PropertyNameResolver.Resolve("year"), "year");
            Assert.AreEqual(PropertyNameResolver.Resolve("fiscal_year"), "fiscal_year");
            Assert.AreEqual(PropertyNameResolver.Resolve("fiscal_quarter"), "fiscal_quarter");
            Assert.AreEqual(PropertyNameResolver.Resolve("2y_beta"), "beta.years_2.beta");
            Assert.AreEqual(PropertyNameResolver.Resolve("3y_beta_count"), "beta.years_3.count");
            Assert.AreEqual(PropertyNameResolver.Resolve("beta.years_2.beta"), "beta.years_2.beta");
        }
    }
}