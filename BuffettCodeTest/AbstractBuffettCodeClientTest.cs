using BuffettCodeAddin.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Buffett
{
    [TestClass]
    public class AbstractBuffettCodeClientTest : AbstractBuffettCodeClient
    {
        [TestMethod]
        public void TestToQuarter()
        {
            Assert.AreEqual("2017Q4", ToQuarter("2017", "4"));
        }

        [TestMethod]
        public void TestToForwardQuarter()
        {
            Assert.AreEqual("2017Q2", ToForwardQuarter("2017", "1", 1));
            Assert.AreEqual("2017Q3", ToForwardQuarter("2017", "2", 1));
            Assert.AreEqual("2017Q4", ToForwardQuarter("2017", "3", 1));
            Assert.AreEqual("2018Q1", ToForwardQuarter("2017", "4", 1));

            Assert.AreEqual("2018Q2", ToForwardQuarter("2017", "4", 2));
            Assert.AreEqual("2018Q3", ToForwardQuarter("2017", "4", 3));
            Assert.AreEqual("2018Q4", ToForwardQuarter("2017", "4", 4));
        }

        [TestMethod]
        public void TestToLowerLimitQuarter()
        {
            Assert.AreEqual("2014Q3", ToLowerLimitQuarter("2017", "1"));
            Assert.AreEqual("2014Q4", ToLowerLimitQuarter("2017", "2"));
            Assert.AreEqual("2015Q1", ToLowerLimitQuarter("2017", "3"));
            Assert.AreEqual("2015Q2", ToLowerLimitQuarter("2017", "4"));
        }

        [TestMethod]
        public void TestToUpperLimitQuarter()
        {
            Assert.AreEqual("2017Q2", ToUpperLimitQuarter("2017", "1"));
            Assert.AreEqual("2017Q3", ToUpperLimitQuarter("2017", "2"));
            Assert.AreEqual("2017Q4", ToUpperLimitQuarter("2017", "3"));
            Assert.AreEqual("2018Q1", ToUpperLimitQuarter("2017", "4"));
        }

        public override Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetQuarter(string apiKey, string ticker, string fiscalYear, string fiscalQuarter, bool isConfigureAwait = true)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetQuarterRange(string apiKey, string ticker, string from, string to, bool isConfigureAwait = true)
        {
            throw new NotImplementedException();
        }
    }
}
