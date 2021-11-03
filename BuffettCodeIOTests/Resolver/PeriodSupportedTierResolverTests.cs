using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeIO.Resolver.Tests
{
    [TestClass()]
    public class PeriodSupportedTierResolverTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod ondemandOldest = FiscalQuarterPeriod.Create(2010, 1);
        private static readonly FiscalQuarterPeriod ondemandLatest = FiscalQuarterPeriod.Create(2021, 4);
        private static readonly PeriodRange<FiscalQuarterPeriod> ondemandTierRange = PeriodRange<FiscalQuarterPeriod>.Create(ondemandOldest, ondemandLatest);
        private static readonly FiscalQuarterPeriod fixedOldest = FiscalQuarterPeriod.Create(2016, 2);
        private static readonly FiscalQuarterPeriod fixedLatest = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly PeriodRange<FiscalQuarterPeriod> fixedTierRange = PeriodRange<FiscalQuarterPeriod>.Create(fixedOldest, fixedLatest);

        private static readonly Company company = Company.Create(ticker, fixedTierRange, ondemandTierRange, PropertyDictionary.Empty(), PropertyDescriptionDictionary.Empty());

        [TestMethod()]
        public void ResolveQuarterTest()
        {
            var supportedDict = new SupportedTierDictionary();
            supportedDict.Add(company);
            var resolver = new PeriodSupportedTierResolver(null, null, supportedDict);

            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedOldest, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedOldest.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.FixedTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, fixedLatest, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandOldest, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandOldest.Next() as FiscalQuarterPeriod, false, false));
            Assert.AreEqual(SupportedTier.OndemandTier, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandLatest, false, false));
            Assert.AreEqual(SupportedTier.None, resolver.Resolve(DataTypeConfig.Quarter, ticker, ondemandLatest.Next() as FiscalQuarterPeriod, false, false));
        }

    }
}