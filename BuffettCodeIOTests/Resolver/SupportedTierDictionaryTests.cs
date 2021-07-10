using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.Resolver.Tests
{
    [TestClass()]
    public class SupportedTierDictionaryTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod ondemandOldest = FiscalQuarterPeriod.Create(2010, 1);
        private static readonly FiscalQuarterPeriod ondemandLatest = FiscalQuarterPeriod.Create(2021, 4);
        private static readonly PeriodRange<FiscalQuarterPeriod> ondemandTierRange = PeriodRange<FiscalQuarterPeriod>.Create(ondemandOldest, ondemandLatest);
        private static readonly FiscalQuarterPeriod fixedOldest = FiscalQuarterPeriod.Create(2016, 2);
        private static readonly FiscalQuarterPeriod fixedLatest = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly PeriodRange<FiscalQuarterPeriod> fixedTierRange = PeriodRange<FiscalQuarterPeriod>.Create(fixedOldest, fixedLatest);


        [TestMethod()]
        public void AddAndHasTest()
        {
            var dict = new SupportedTierDictionary();
            var company = Company.Create(ticker, fixedTierRange, ondemandTierRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

            // at first, not contained
            Assert.IsFalse(dict.Has(ticker, DataTypeConfig.Quarter));
            dict.Add(company);
            // check added
            Assert.IsTrue(dict.Has(ticker, DataTypeConfig.Quarter));
        }

        [TestMethod()]
        public void GetTest()
        {
            var dict = new SupportedTierDictionary();
            // throw Key NotFound 
            Assert.ThrowsException<KeyNotFoundException>(() => dict.Get(ticker, fixedLatest));

            var company = Company.Create(ticker, fixedTierRange, ondemandTierRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());
            dict.Add(company);

            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedOldest));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedOldest.Next() as FiscalQuarterPeriod));
            Assert.AreEqual(SupportedTier.FixedTier, dict.Get(ticker, fixedLatest));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandOldest));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandOldest.Next() as FiscalQuarterPeriod));
            Assert.AreEqual(SupportedTier.OndemandTier, dict.Get(ticker, ondemandLatest));
            Assert.AreEqual(SupportedTier.None, dict.Get(ticker, ondemandLatest.Next() as FiscalQuarterPeriod));
        }
    }
}